#ifndef INPUTDEVICE_H
#define INPUTDEVICE_H

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

#include"globals.h"

int InitInputDevice();
void KeyPressed(int fd_key_emulator, int code);
void KeyReleased(int fd_key_emulator, int code);
static void setup_abs(int fd, unsigned chan, int min, int max);
void* DeviceThread(void*);
#endif

