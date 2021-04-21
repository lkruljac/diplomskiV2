import RPi.GPIO as GPIO

class Car:

    def __init__(self):
        global RPWM
        self.RPWM = 9
        global RIN1 
        RIN1 = 10
        global RIN2 
        RIN2 = 11
        global LIN1 
        LIN1 = 23
        global LIN2 
        LIN2 = 24
        global LPWM
        LPWM = 25

        GPIO.setup(RPWM, GPIO.OUT)
        GPIO.setup(RIN1, GPIO.OUT)
        GPIO.setup(RIN2, GPIO.OUT)

        GPIO.setup(LIN1, GPIO.OUT)
        GPIO.setup(LIN2, GPIO.OUT)
        GPIO.setup(LPWM, GPIO.OUT)


    def drive(direction, rspeed, lspeed):
        if direction > 0:
            GPIO.OUT(RIN1, 0)
            GPIO.output(RIN2, 1)
            GPIO.output(self.LIN1, 0)
            GPIO.output(LIN2, 1)
        else:
            GPIO.output(RIN1, 1)
            GPIO.output(RIN2, 0)
            GPIO.output(LIN1, 1)
            GPIO.output(LIN2, 0)
