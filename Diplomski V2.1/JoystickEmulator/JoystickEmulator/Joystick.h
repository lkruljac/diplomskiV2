#ifndef JOYSTICK_H
#define JOYSTICK_H

#include <cstdlib>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <fcntl.h>
#include <errno.h>
#include <linux/input.h>
#include <linux/uinput.h>
#include <iostream>

#include <time.h>
#include <string>


#define msleep(ms) usleep((ms)*1000)

typedef struct JoystickButtonsState{
	uint8_t axis_X;
	uint8_t axis_Y;
	uint8_t axis_RY;
	uint8_t axis_RX;

	uint8_t ABXY;
	uint8_t DPAD;
	uint8_t rest;
}JoystickButtonsState;


int InitJoystickDevice(int *JoystickDescriptor);
int WriteAndSyncEvent(int JoystickDescriptor, JoystickButtonsState buttonsState);
int WriteSyncEvent(int JoystickDescriptor);
static void setup_abs(int FileDescriptor, unsigned chan, int min, int max);

input_event* GenerateEvent(JoystickButtonsState bs);

#endif // !JOYSTICK_H


