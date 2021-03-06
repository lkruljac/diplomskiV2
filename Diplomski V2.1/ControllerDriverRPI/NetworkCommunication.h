#pragma once

#include <iostream>
#include <sys/types.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <string.h>
#include <string>



using namespace std;


char* getLocalIp(char* netAdapter);
void* NetworkThread(void*);
int NetworkCommunication_Start(uint16_t port);
int RecieveData(int* dataBuffer, int size);
int IsAdapterConnected(char* networkAdapter);
int EstablishConnection(char* networkAdapter);
