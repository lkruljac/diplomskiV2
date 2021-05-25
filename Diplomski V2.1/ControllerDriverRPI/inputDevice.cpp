#include"inputDevice.h"

using namespace std;



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


void JoystickEvent(int fd_key_emulator, Message msg)
{
	JoystickButtonsState bs;
	//parse msg
	bs.axis_X = (uint8_t)msg.content[1];
	bs.axis_Y = (uint8_t)msg.content[2];

	bs.axis_RX = (uint8_t)msg.content[4];
	bs.axis_RY = (uint8_t)msg.content[5];

	bs.ABXY = (uint8_t)((0b00001111)&(0b11110000 & msg.content[6]) >> 4);
	bs.DPAD = (uint8_t)((0b00001111 & msg.content[6]));

	bs.rest = (uint8_t)msg.content[7];

	WriteAndSyncEvent(fd_key_emulator, bs);

}




void* DeviceThread(void*) {
	
	int fd_key_emulator;
	if (InitKeyboardDevice(&fd_key_emulator)) 
	{
		printf("Errror opening keyboard\n");
	}


	int joystick_emulator;
	if (InitJoystickDevice(&joystick_emulator)) 
	{
		printf("Errror opening joystick\n");
	}


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
				JoystickEvent(joystick_emulator, msg);
			}
			recivedEventFlag = 0;
		}
	}
}


