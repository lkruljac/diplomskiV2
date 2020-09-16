#include <stdlib.h>
#include <string.h>
#include <error.h>
#include <argp.h>


typedef enum ConnectionType
{
    uart,
    bluetooth,
    network
}ConnectionType;

#define OPT_NO_OPT (-1)
typedef struct
{
    char** programArgs;
    int programArgsCounter;
    int versionFlag;
    int uartFlag;
    int bluetoothFlag;
    int networkFlag;
    ConnectionType connectionLinkType;
}ARGUMENTS;

//depending on struct above, implement a dump function for test
void argumentDump(ARGUMENTS arguments);





void ArgpParser(int argc, char** argv, ARGUMENTS* parsedArgs);
