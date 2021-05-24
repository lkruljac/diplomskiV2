#include"Joystick.h"
int main(void)
{

	int JoystickDescriptor;
	if (InitJoystickDevice(&JoystickDescriptor) != 0) {
		return 0;
	}


	while (true) 
	{
		static JoystickButtonsState bs;
		memset(&bs, 0, sizeof(bs));
		bs.rest = rand() / 256;
		WriteAndSyncEvent(JoystickDescriptor, bs);
	}




	return 0;
}