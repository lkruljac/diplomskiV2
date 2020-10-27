#include <iostream>
#include <sys/types.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <string.h>
#include <string>
#include <pthread.h>

#include "argumentParser.h"
#include "NetworkCommunication.h"
#include "inputDevice.h"

using namespace std;


//globals
pthread_t thread_NetworkListening;
pthread_t thread_DeviceEventHandler;

int main(int argc, char** argv)
{
    //Parsing arguments
    ARGUMENTS arguments;
    ArgpParser(argc, argv, &arguments);
    argumentDump(arguments);
   
    if (arguments.versionFlag) {
        printf("RPI (virtual) controller driver", argp_program_version);
        exit(0);
    }
    // Checking is more option selected, or there is no selected option
    if ((arguments.bluetoothFlag + arguments.uartFlag + arguments.networkFlag) != 1) {
        printf("Not selected connection type or selected more than one\n");
        exit(1);
    }


    switch(arguments.connectionLinkType) {
        case(uart):

            break;
        
        case(bluetooth):
            
            break;

        case(network):
        {
            int port = 54000;
            char *ip;
            ip = strdup(getLocalIp(arguments.networkAdapter));

            if (strcmp(ip, "0.0.0.0") == 0) {
                printf("There is no established connection on network Adapter: \"%s\"\n", arguments.networkAdapter);
                exit(1);
            }

            printf("Starting Network communication!\n");
            printf("To set connection please use:\n\tIP: %s,\n\tport: %d\n\n", ip, port);
            if (NetworkCommunication_Start((uint16_t)port) == 0) {
                printf("Communication over network established!\n");
            }     
            else {
                printf("Can not establish connection!\n");
                exit(1);
            }
            printf("\nStarting NetworkListeningThread!\n\n");
            pthread_create(&thread_NetworkListening, NULL, NetworkThread, NULL);
            break;
        }
            
        default:
            printf("Something went wrong with connection type\n");
            exit(1);
            break;
    }

    pthread_create(&thread_DeviceEventHandler, NULL, DeviceThread, NULL);
    printf("Pres ctrl + c to terminate...\n");
    while (true) {

    }

    return 0;
}