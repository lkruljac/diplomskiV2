#ifndef GLOBALS_H
#define GLOBALS_H

#include "NetworkCommunication.h"


typedef struct Message {
	short int code;
	short int deviceType;
	short int eventType;
}Message;

extern int clientSocket;

extern bool recivedEventFlag;
extern Message msg;
#endif // !GLOBALS_H
