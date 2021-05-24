#ifndef GLOBALS_H
#define GLOBALS_H

#include "NetworkCommunication.h"


typedef struct Message {
	int deviceType;
	int eventType;
	int keyCode;
	int scanCode;
	int size;
	uint8_t* content;
}Message;

extern int clientSocket;

extern bool recivedEventFlag;
extern Message msg;
#endif // !GLOBALS_H
