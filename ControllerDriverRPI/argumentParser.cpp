#include "argumentParser.h"

/* The options we understand. */
static struct argp_option options[] = {

        {"version", 'v', 0, 0, "show program's version number and exit\n" },
        {"bluetooth", 'b', 0, 0, "Start driver for bluetooth virtaul controller\n"},
        {"netowrk", 'n', 0, 0, "Start driver for network virtaul controller\n" },
        {"uart", 'u', 0, 0, "Start driver for uart virtaul controller\n"},
        { 0 }
};


/* Program documentation. */
static char doc[] = \
"This program is used as driver for controllerEmulator\n";

/* A description of the arguments we accept. */
static char args_doc[] = "[STRING...]";

void argumentDump(ARGUMENTS arguments)
{
    printf("Argument dump:\n");
    printf("FLAGS:\n\
            VersionFlag:\t%d\n\
            UARTFlag(0):\t\t%d\n\
            BluetoothFlag(1):\t%d\n\
            NetworkFlag(2):\t%d\n\
            Connection type:\t\%d\n\n",
            arguments.versionFlag,
            arguments.uartFlag,
            arguments.bluetoothFlag,
            arguments.networkFlag,
            arguments.connectionLinkType);
}

/* Parse a single option. */
static error_t parse_opt(int key, char* arg, struct argp_state* state)
{
    /* Get the input argument from argp_parse, which we
       know is a pointer to our arguments structure. */
    ARGUMENTS* arguments = (ARGUMENTS*) state->input;

    //used for multiple args for single option
    static int lastOption = OPT_NO_OPT;

    //printf("\n%s", arg);

    switch (key)
    {
        //flags:
    case 'v':
        arguments->versionFlag = 1;

        lastOption = key;
        break;
    case 'u':
        arguments->uartFlag = 1;
        arguments->connectionLinkType = uart;
        lastOption = key;
        break;

    case 'b':
        arguments->bluetoothFlag = 1;
        arguments->connectionLinkType = bluetooth;
        lastOption = key;
        break;

    case 'n':
        arguments->networkFlag = 1;
        arguments->connectionLinkType = network;
        lastOption = key;
        break;

    case ARGP_KEY_NO_ARGS:
        break;

    case ARGP_KEY_ARG:
        // Here in variable arg is stored each uncaught string
        // This case repeat for every string singly, in same order as it passed from comand (ARGP_IN_ORDER flag)
        switch (lastOption) {
        case (OPT_NO_OPT):
            arguments->programArgs = (char**)realloc(arguments->programArgs, (arguments->programArgsCounter + 1) * sizeof(char*));

            arguments->programArgs[arguments->programArgsCounter] = strdup(arg);
            arguments->programArgsCounter++;
            break;
        
        default:
            //for option that does not suport multiple args but they got
            printf("There was uncaught arg \"%s\"\n", arg);
        }
        break;

    default:
        return ARGP_ERR_UNKNOWN;
    }
    return 0;
}

/* Our argp parser. */
static struct argp argp = { options, parse_opt, args_doc, doc };

void ArgpParser(int argc, char** argv, ARGUMENTS* parsedArgs)
{
    ARGUMENTS arguments;
    arguments.programArgs = NULL;
    arguments.programArgsCounter = 0;
    arguments.versionFlag = 0;
    arguments.bluetoothFlag = 0;
    arguments.networkFlag = 0;
    arguments.uartFlag = 0;

    //ARGP_IN_ORDER is important because of multiple args for single option logic in parse_opt()
    argp_parse(&argp, argc, argv, ARGP_IN_ORDER, 0, &arguments);

    //returning parsed arguments to main()
    *parsedArgs = arguments;
}