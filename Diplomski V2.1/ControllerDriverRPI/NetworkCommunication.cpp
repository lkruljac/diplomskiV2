#include <iostream>
#include <sys/types.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <string.h>
#include <stdio.h>
#include <string.h>
#include <sys/ioctl.h>
#include <netinet/in.h>
#include <net/if.h>

#include "NetworkCommunication.h"
#include "globals.h"




using namespace std;

char* getLocalIp(char* netAdapter)
{
	int fd;
	struct ifreq ifr;

	fd = socket(AF_INET, SOCK_DGRAM, 0);

	ifr.ifr_addr.sa_family = AF_INET;

	snprintf(ifr.ifr_name, IFNAMSIZ, netAdapter);

	ioctl(fd, SIOCGIFADDR, &ifr);


	close(fd);

	return inet_ntoa(((struct sockaddr_in*)&ifr.ifr_addr)->sin_addr);
}

int NetworkCommunication_Start(uint16_t port) {

	// Create a socket
	int listening = socket(AF_INET, SOCK_STREAM, 0);
	if (listening == -1)
	{
		cerr << "Can't create a socket! Quitting" << endl;
		return -1;
	}

	// Bind the ip address and port to a socket
	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(port);
	inet_pton(AF_INET, "0.0.0.0", &hint.sin_addr);

	bind(listening, (sockaddr*)&hint, sizeof(hint));

	// Tell Winsock the socket is for listening
	listen(listening, SOMAXCONN);

	// Wait for a connection
	sockaddr_in client;
	socklen_t clientSize = sizeof(client);

	clientSocket = accept(listening, (sockaddr*)&client, &clientSize);

	char host[NI_MAXHOST];      // Client's remote name
	char service[NI_MAXSERV];   // Service (i.e. port) the client is connect on

	memset(host, 0, NI_MAXHOST); // same as memset(host, 0, NI_MAXHOST);
	memset(service, 0, NI_MAXSERV);

	if (getnameinfo((sockaddr*)&client, sizeof(client), host, NI_MAXHOST, service, NI_MAXSERV, 0) == 0)
	{
		cout << host << " connected on port " << service << endl;
	}
	else
	{
		inet_ntop(AF_INET, &client.sin_addr, host, NI_MAXHOST);
		cout << host << " connected on port " << ntohs(client.sin_port) << endl;
	}

	// Close listening socket
	close(listening);

	// While loop: accept and echo message back to client
	char buf[4096];

	memset(buf, 0, 4096);

	// Wait for client to send data
	int bytesReceived = recv(clientSocket, buf, 4096, 0);
	if (bytesReceived == -1)
	{
		cerr << "Error in recv(). Quitting" << endl;
		return 1;
	}

	if (bytesReceived == 0)
	{
		cout << "Client disconnected " << endl;
		return 1;
	}

	cout << string(buf, 0, bytesReceived) << endl;

	// Echo message back to client
	send(clientSocket, buf, bytesReceived + 1, 0);

	return 0;
}

void* NetworkThread(void*) {

	printf("Network thread started:\n");
	// While loop: accept message
	while (true)
	{
		//Get Device Type
		int size = 4;
		if (size != RecieveData(&msg.deviceType, size))
		{
			if (size == -2) {
				continue;
			}
			break;
		}


		//Keyboard
		if (msg.deviceType == 1)
		{
			//event Type
			if (size != RecieveData(&msg.eventType, size))
			{
				break;
			}
			//keyCode
			if (size != RecieveData(&msg.keyCode, size))
			{
				break;
			}
			//scanCode
			if (size != RecieveData(&msg.scanCode, size))
			{
				break;
			}
		}

		//Joystick
		if (msg.deviceType == 3)
		{
			//size
			if (size != RecieveData(&msg.size, size))
			{
				break;
			}

			//Content
			free(msg.content);
			msg.content = (uint8_t*)malloc(msg.size * sizeof(uint8_t));
			if (msg.size != RecieveData((int*)msg.content, msg.size))
			{
				break;
			}
		}

		recivedEventFlag = 1;
	}
	return nullptr;

}


int RecieveData(int* dataBuffer, int size)
{

	int bytesReceived = recv(clientSocket, dataBuffer, size, 0);
	if (bytesReceived == -1)
	{
		cerr << "Error in recv(). Quitting" << endl;
		return -1;
	}
	if (bytesReceived == 0)
	{
		cout << "Client disconnected. Please reconnect" << endl;
		while (EstablishConnection("eth0") != 0)
		{
			printf("Establisihng connection failed, try again!");
		}
		return -2;
	}
	return bytesReceived;
}

int IsAdapterConnected(char* networkAdapter) {
	char* ip;
	ip = strdup(getLocalIp(networkAdapter));
	if (strcmp(ip, "0.0.0.0") == 0) {
		printf("There is no established connection on network Adapter: \"%s\"\n", networkAdapter);
		return -1;
	}
	return 1;
}

int EstablishConnection(char* networkAdapter) {
	int port = 54000;
	char* ip;
	ip = strdup(getLocalIp(networkAdapter));

	printf("To set connection please use:\n\tIP: %s,\n\tport: %d\n\n", ip, port);
	if (NetworkCommunication_Start((uint16_t)port) == 0) {
		printf("Communication over network established!\n");
	}
	else {
		printf("Can not establish connection!\n");
		return -1;
	}
	return 0;
}

