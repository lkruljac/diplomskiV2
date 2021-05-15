#include"inputDevice.h"

using namespace std;

int InitKeyboardDevice() {

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
	key_input_event.code = (__u16)code;
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

void KeyboardEvent(int fd_key_emulator, Message msg)
{
	if (msg.eventType == 0)
	{
		KeyPressed(fd_key_emulator, msg.scanCode);
	}
	else
	{
		KeyReleased(fd_key_emulator, msg.scanCode);
	}
}

int InitJoystickDevice()
{
	int fd = open("/dev/uinput", O_WRONLY | O_NONBLOCK); //opening of uinput
	if (fd < 0) {
		printf("Opening of joystikc uinput failed!\n");
		return 1;
	}


	ioctl(fd, UI_SET_EVBIT, EV_KEY); //setting Gamepad keys
	ioctl(fd, UI_SET_KEYBIT, BTN_A);
	ioctl(fd, UI_SET_KEYBIT, BTN_B);
	ioctl(fd, UI_SET_KEYBIT, BTN_X);
	ioctl(fd, UI_SET_KEYBIT, BTN_Y);
	ioctl(fd, UI_SET_KEYBIT, BTN_TL);
	ioctl(fd, UI_SET_KEYBIT, BTN_TR);
	ioctl(fd, UI_SET_KEYBIT, BTN_TL2);
	ioctl(fd, UI_SET_KEYBIT, BTN_TR2);
	ioctl(fd, UI_SET_KEYBIT, BTN_START);
	ioctl(fd, UI_SET_KEYBIT, BTN_SELECT);
	ioctl(fd, UI_SET_KEYBIT, BTN_THUMBL);
	ioctl(fd, UI_SET_KEYBIT, BTN_THUMBR);
	ioctl(fd, UI_SET_KEYBIT, BTN_DPAD_UP);
	ioctl(fd, UI_SET_KEYBIT, BTN_DPAD_DOWN);
	ioctl(fd, UI_SET_KEYBIT, BTN_DPAD_LEFT);
	ioctl(fd, UI_SET_KEYBIT, BTN_DPAD_RIGHT);
	ioctl(fd, UI_SET_EVBIT, EV_ABS); //setting Gamepad thumbsticks
	ioctl(fd, UI_SET_ABSBIT, ABS_X);
	ioctl(fd, UI_SET_ABSBIT, ABS_Y);
	ioctl(fd, UI_SET_ABSBIT, ABS_RX);
	ioctl(fd, UI_SET_ABSBIT, ABS_RY);

	struct uinput_user_dev uidev; //setting the default settings of Gamepad
	memset(&uidev, 0, sizeof(uidev));
	snprintf(uidev.name, UINPUT_MAX_NAME_SIZE, "Simple Gamepad"); //Name of Gamepad
	uidev.id.bustype = BUS_USB;
	uidev.id.vendor = 0x3;
	uidev.id.product = 0x3;
	uidev.id.version = 2;

	uidev.absmax[ABS_X] = 512; //Parameters of thumbsticks
	uidev.absmin[ABS_X] = -512;
	uidev.absfuzz[ABS_X] = 0;
	uidev.absflat[ABS_X] = 15;
	uidev.absmax[ABS_Y] = 512;
	uidev.absmin[ABS_Y] = -512;
	uidev.absfuzz[ABS_Y] = 0;
	uidev.absflat[ABS_Y] = 15;
	uidev.absmax[ABS_RX] = 512;
	uidev.absmin[ABS_RX] = -512;
	uidev.absfuzz[ABS_RX] = 0;
	uidev.absflat[ABS_RX] = 16;
	uidev.absmax[ABS_RY] = 512;
	uidev.absmin[ABS_RY] = -512;
	uidev.absfuzz[ABS_RY] = 0;
	uidev.absflat[ABS_RY] = 16;
	if (write(fd, &uidev, sizeof(uidev)) < 0) //writing settings
	{
		printf("error: write");
		return 1;
	}
	if (ioctl(fd, UI_DEV_CREATE) < 0) //writing ui dev create
	{
		printf("error: ui_dev_create");
		return 1;
	}




}

int JoystickEvent(int JoystickEmulator, Message msg)
{
	static int counter = 0;
	printf("Here I am %d\n", ++counter);

	struct input_event ev;
	unsigned char toggle = 0;
	memset(&ev, 0, sizeof(struct input_event)); //setting the memory for event
	ev.type = EV_ABS;
	ev.code = BTN_THUMBL;
	ev.value = !toggle;
	toggle = !toggle;
	if (write(JoystickEmulator, &ev, sizeof(struct input_event)) < 0) //writing the key change
	{
		printf("error: key-write");
		return 1;
	}
	memset(&ev, 0, sizeof(struct input_event)); //setting the memory for event
	ev.type = EV_ABS;
	ev.code = ABS_Y;
	ev.value = counter;
	if (write(JoystickEmulator, &ev, sizeof(struct input_event)) < 0) //writing the thumbstick change
	{
		printf("error: thumbstick-write");
		return 1;
	}
	memset(&ev, 0, sizeof(struct input_event));
	ev.type = EV_SYN;
	ev.code = SYN_REPORT;
	ev.value = 0;
	if (write(JoystickEmulator, &ev, sizeof(struct input_event)) < 0) //writing the sync report
	{
		printf("error: sync-report");
		return 1;
	}
	printf("Done joystick event\n");
}





void* DeviceThread(void*) {
	int fd_key_emulator = InitKeyboardDevice();
	int joystick_emulator = InitJoystickDevice();
	while (true)
	{
		if (recivedEventFlag)
		{
			//printf("Recieved\n");
			//keyboard
			if (msg.deviceType == 1)
			{
				KeyboardEvent(fd_key_emulator, msg);
			}
			//Joystick
			if (msg.deviceType == 3)
			{
				JoystickEvent(fd_key_emulator, msg);
			}
			usleep(10);
			recivedEventFlag = 0;

		}
	}
}


static void setup_abs(int fd, unsigned chan, int min, int max)
{
	if (ioctl(fd, UI_SET_ABSBIT, chan))
		perror("UI_SET_ABSBIT");

	struct uinput_abs_setup s =
	{
	 .code = chan,
	 .absinfo = {.minimum = min,  .maximum = max },
	};

	if (ioctl(fd, UI_ABS_SETUP, &s))
		perror("UI_ABS_SETUP");
}