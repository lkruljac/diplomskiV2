#ifndef KEYBOARD_H
#define KEYBOARD_H

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



int InitKeyboardDevice(int* KeyboardDescriptor);

void KeyPressed(int fd_key_emulator, int code);

void KeyReleased(int fd_key_emulator, int code);


#endif