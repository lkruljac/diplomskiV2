#include"Joystick.h"

int InitJoystickDevice(int* FileDescriptor)
{
	int fd = open("/dev/uinput", O_WRONLY | O_NONBLOCK | O_CREAT);

	if (fd < 0)
	{
		perror("open /dev/uinput");
		return 1;
	}

	ioctl(fd, UI_SET_EVBIT, EV_KEY); // enable button/key handling
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
	ioctl(fd, UI_SET_EVBIT, EV_ABS); // enable analog absolute position handling
	setup_abs(fd, ABS_X, 0, 256);
	setup_abs(fd, ABS_Y, 0, 256);
	setup_abs(fd, ABS_RX, 0, 256);
	setup_abs(fd, ABS_RY, 0, 256);


	struct uinput_setup setup =
	{
		 .id =
		 {
		  .bustype = BUS_USB,
		  .vendor = 0x3,
		  .product = 0x3,
		  .version = 2,
		 },
		.name = {'c'}
	};

	if (ioctl(fd, UI_DEV_SETUP, &setup))
	{
		perror("UI_DEV_SETUP");
		return 1;
	}
	if (ioctl(fd, UI_DEV_CREATE))
	{
		perror("UI_DEV_CREATE");
		return 1;
	}
	*FileDescriptor = fd;
	return 0;
}

int WriteAndSyncEvent(int JoystickDescriptor,  JoystickButtonsState buttonsState)
{
	static int counter;
	input_event* ev = GenerateEvent(buttonsState);
	//WriteAndSyncEvent(JoystickDescripotr, ev);
	if (write(JoystickDescriptor, ev, sizeof(input_event) * 21) < 0)
	{
		perror("write");
		return 1;
	}
	msleep(5);
	//No error
	printf("Success %d\n", counter++);
	return 0;
}

int WriteSyncEvent(int JoystickDescriptor) 
{
	input_event event;
	memset(&event, 0, sizeof(struct input_event));
	event.type = EV_SYN;
	event.code = SYN_REPORT;
	event.value = 0;
	if (write(JoystickDescriptor, &event, sizeof(struct input_event)) < 0) //writing the sync report
	{
		printf("error: sync-report");
		return 1;
	}
	return 0;
}


static void setup_abs(int FileDescriptor, unsigned chan, int min, int max)
{
	if (ioctl(FileDescriptor, UI_SET_ABSBIT, chan))
		perror("UI_SET_ABSBIT");

	struct uinput_abs_setup s =
	{
	 .code = chan,
	 .absinfo = {.minimum = min,  .maximum = max },
	};

	if (ioctl(FileDescriptor, UI_ABS_SETUP, &s))
		perror("UI_ABS_SETUP");
}


input_event* GenerateEvent(JoystickButtonsState bs) {
	struct input_event *ev;
	ev = (input_event*)calloc(21, sizeof(input_event));

	#pragma region ABXY
	//ABXY
	ev[0].type = EV_KEY;
	ev[0].code = BTN_A;
	ev[0].value = 0b0100 & bs.ABXY;

	ev[1].type = EV_KEY;
	ev[1].code = BTN_B;
	ev[1].value = 0b0010 & bs.ABXY;

	ev[2].type = EV_KEY;
	ev[2].code = BTN_X;
	ev[2].value = 0b1000 & bs.ABXY;

	ev[3].type = EV_KEY;
	ev[3].code = BTN_Y;
	ev[3].value = 0b0001 & bs.ABXY;
	#pragma endregion ABXY

	#pragma region Axis
	//AXIS
	ev[4].type = EV_ABS;
	ev[4].code = ABS_Y;
	ev[4].value = bs.axis_Y;

	ev[5].type = EV_ABS;
	ev[5].code = ABS_X;
	ev[5].value = bs.axis_X;

	ev[6].type = EV_ABS;
	ev[6].code = ABS_RY;
	ev[6].value = bs.axis_RY;

	ev[7].type = EV_ABS;
	ev[7].code = ABS_RX;
	ev[7].value = bs.axis_RX;
	#pragma endregion Axis

	#pragma region DPAD
	ev[8].type = EV_KEY;
	ev[8].code = BTN_DPAD_UP;
	if (bs.DPAD == 0 ||
		bs.DPAD == 1 ||
		bs.DPAD == 7) {
		ev[8].value = 0b10000000;
	}

	ev[9].type = EV_KEY;
	ev[9].code = BTN_DPAD_RIGHT;
	if (bs.DPAD == 1 ||
		bs.DPAD == 2 ||
		bs.DPAD == 3) {
		ev[9].value = 0b10000000;
	}

	ev[10].type = EV_KEY;
	ev[10].code = BTN_DPAD_DOWN;
	if (bs.DPAD == 3 ||
		bs.DPAD == 4 ||
		bs.DPAD == 5) {
		ev[10].value = 0b10000000;
	}

	ev[11].type = EV_KEY;
	ev[11].code = BTN_DPAD_LEFT;
	if (bs.DPAD == 5 ||
		bs.DPAD == 6 ||
		bs.DPAD == 7) {
		ev[1].value = 0b10000000;
	}

	#pragma endregion DPAD


	#pragma region rest
	ev[12].type = EV_KEY;
	ev[12].code = BTN_TL;
	ev[12].value = 0b00000001 & bs.rest;

	ev[13].type = EV_KEY;
	ev[13].code = BTN_TR;
	ev[13].value = 0b00000010 & bs.rest;

	ev[14].type = EV_KEY;
	ev[14].code = BTN_TL2;
	ev[14].value = 0b00000100 & bs.rest;

	ev[15].type = EV_KEY;
	ev[15].code = BTN_TR2;
	ev[15].value = 0b00001000 & bs.rest;

	ev[16].type = EV_KEY;
	ev[16].code = BTN_SELECT;
	ev[16].value = 0b00010000 & bs.rest;

	ev[17].type = EV_KEY;
	ev[17].code = BTN_START;
	ev[17].value = 0b00100000 & bs.rest;

	ev[18].type = EV_KEY;
	ev[18].code = BTN_THUMBL;
	ev[18].value = 0b01000000 & bs.rest;

	ev[19].type = EV_KEY;
	ev[19].code = BTN_THUMBR;
	ev[19].value = 0b10000000 & bs.rest;
	#pragma endregion rest
	// sync event tells input layer we're done with a "batch" of
	// updates
	ev[20].type = EV_SYN;
	ev[20].code = SYN_REPORT;
	ev[20].value = 0;

	return ev;
}