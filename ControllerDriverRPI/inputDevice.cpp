#include"inputDevice.h"

using namespace std;

int InitInputDevice() {

    // open file descriptor
    int fd_key_emulator = open("/dev/uinput", O_WRONLY | O_NONBLOCK);
    if (fd_key_emulator < 0)
    {
        std::cout << "error in open : " << strerror(errno) << std::endl;
    }

    // uinput_user_dev struct for fake keyboard
    struct uinput_user_dev dev_fake_keyboard;
    memset(&dev_fake_keyboard, 0, sizeof(uinput_user_dev));
    snprintf(dev_fake_keyboard.name, UINPUT_MAX_NAME_SIZE, "kb-emulator");
    dev_fake_keyboard.id.bustype = BUS_USB;
    dev_fake_keyboard.id.vendor = 0x01;
    dev_fake_keyboard.id.product = 0x01;
    dev_fake_keyboard.id.version = 1;



    /**configure the input device to send type of events, inform to subsystem which
     * type of input events we are using via ioctl calls.
     * UI_SET_EVBIT ioctl request is used to applied on uinput descriptor to enable a type of event.
     **/
     // enable key press/release event
    if (ioctl(fd_key_emulator, UI_SET_EVBIT, EV_KEY))
    {
        std::cout << "Error in ioctl : UI_SET_EVBIT : EV_KEY : " << strerror(errno) << std::endl;
    }

    // enable set of KEY events here
    int i;
    for (i = 1; i < 248; i++) {
        if (ioctl(fd_key_emulator, UI_SET_KEYBIT, i))
        {
            std::cout << "Error in ioctl : UI_SET_KEYBIT : " << i << strerror(errno) << std::endl;
        }
    }



    // enable synchronization event
    if (ioctl(fd_key_emulator, UI_SET_EVBIT, EV_SYN))
    {
        std::cout << "Error in ioctl : UI_SET_EVBIT : EV_SYN : " << strerror(errno) << std::endl;
    }

    // now write the uinput_user_dev structure into uinput file descriptor
    if (write(fd_key_emulator, &dev_fake_keyboard, sizeof(uinput_user_dev)) < 0)
    {
        std::cout << "Error in write(): uinput_user_dev struct into uinput file descriptor: " << strerror(errno) << std::endl;
    }

    // create the device via an IOCTL call 
    if (ioctl(fd_key_emulator, UI_DEV_CREATE))
    {
        std::cout << "Error in ioctl : UI_DEV_CREATE : " << strerror(errno) << std::endl;
    }
    // now fd_key_emulator represents the end-point file descriptor of the new input device. 

    sleep(1);
    return fd_key_emulator;

}

void KeyPressed(int fd_key_emulator, int code) {
    // struct member for input events
    struct input_event key_input_event;
    memset(&key_input_event, 0, sizeof(input_event));

    // key press event for 'a'
    key_input_event.type = EV_KEY;
    key_input_event.code = (__u16) code;
    key_input_event.value = 1;

    // now write to the file descriptor
    if (write(fd_key_emulator, &key_input_event, sizeof(input_event)) < 0)
    {
        std::cout << "Error write : KEY_A press : " << strerror(errno) << std::endl;
    }

    memset(&key_input_event, 0, sizeof(input_event));
    // EV_SYN for key press event
    key_input_event.type = EV_SYN;
    key_input_event.code = SYN_REPORT;
    key_input_event.value = 0;

    // now write to the file descriptor
    if (write(fd_key_emulator, &key_input_event, sizeof(input_event)) < 0)
    {
        std::cout << "Error write : EV_SYN for key press : " << strerror(errno) << std::endl;
    }

    memset(&key_input_event, 0, sizeof(input_event));
}

void KeyReleased(int fd_key_emulator, int code) {
    // struct member for input events
    struct input_event key_input_event;
    memset(&key_input_event, 0, sizeof(input_event));

    // key release event for 'a'
    key_input_event.type = EV_KEY;
    key_input_event.code = (__u16)code;
    key_input_event.value = 0;

    // now write to the file descriptor
    if (write(fd_key_emulator, &key_input_event, sizeof(input_event)) < 0)
    {
        std::cout << "Error write : KEY_A release : " << strerror(errno) << std::endl;
    }

    memset(&key_input_event, 0, sizeof(input_event));
    // EV_SYN for key press event
    key_input_event.type = EV_SYN;
    key_input_event.code = SYN_REPORT;
    key_input_event.value = 0;

    // now write to the file descriptor
    if (write(fd_key_emulator, &key_input_event, sizeof(input_event)) < 0)
    {
        std::cout << "Error write : EV_SYN for key release : " << strerror(errno) << std::endl;
    }
}

void ParseEvent(char *event, char* key, char* deviceType, int* eventType) {
    sscanf(event, "%s_%s_%d", key, deviceType, eventType);
}

void* DeviceThread(void*) {
    char key[10];
    char deviceType[10];
    int eventType;
    int fd_key_emulator = InitInputDevice();



    while (true) {
        if (recivedEvent != NULL) {
            //printf("Device got: %s\n", recivedEvent);
            ParseEvent(recivedEvent, key, deviceType, &eventType);
            if (eventType) {
                KeyPressed(fd_key_emulator, KEY_A);
            }
            else {
                KeyReleased(fd_key_emulator, KEY_A);
            }
            free(recivedEvent);
            recivedEvent = NULL;
        }
    }
}