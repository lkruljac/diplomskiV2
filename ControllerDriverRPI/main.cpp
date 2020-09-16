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

using namespace std;

int main(int argc, char** argv)
{
    //Parsing arguments
    ARGUMENTS arguments;
    ArgpParser(argc, argv, &arguments);
    argumentDump(arguments);
    
    //main starts here
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

        case(bluetooth):
            
            break;

        case(network):
            NetworkCommunication_Main(54000);
            break;

        case(uart):
            
            break;

        default:
            printf("Something went wrong with connection type\n");
            exit(1);
            break;
    }

    

    return 0;
}