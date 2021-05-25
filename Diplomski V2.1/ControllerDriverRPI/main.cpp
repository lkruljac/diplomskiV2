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
            printf("UART is not suported in this version.\n");
            return -1;
            break;
        
        case(bluetooth):
            printf("Bluetooth is not suported in this version.\n");
            return -1;
            break;

        case(network):
        {
            if (IsAdapterConnected(arguments.networkAdapter) != 1) {
                exit(1);
            }
            //try to establich connection
            while (EstablishConnection(arguments.networkAdapter) != 0)
            {
                printf("Establisihng connection failed, try again!");
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