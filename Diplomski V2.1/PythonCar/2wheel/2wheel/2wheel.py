import RPi.GPIO as GPIO
from time import sleep

GPIO.setmode(GPIO.BCM)

#left motor
in2 = 10    
in1 = 9     
en1 = 11    

#right motor
in3 = 24    
in4 = 23    
en2 = 25    

#setup motors 
GPIO.setup(in2,GPIO.OUT)
GPIO.setup(in1,GPIO.OUT)
GPIO.setup(en1,GPIO.OUT)

GPIO.setup(in3,GPIO.OUT)
GPIO.setup(in4,GPIO.OUT)
GPIO.setup(en2,GPIO.OUT)

def forwards():
    GPIO.output(in2,GPIO.HIGH)
    GPIO.output(in1,GPIO.LOW)
    GPIO.output(en1,GPIO.HIGH)

    GPIO.output(in3,GPIO.HIGH)
    GPIO.output(in4,GPIO.LOW)
    GPIO.output(en2,GPIO.HIGH)

def backwards():
    GPIO.output(in2,GPIO.LOW)
    GPIO.output(in1,GPIO.HIGH)
    GPIO.output(en1,GPIO.HIGH)

    GPIO.output(in3,GPIO.LOW)
    GPIO.output(in4,GPIO.HIGH)
    GPIO.output(en2,GPIO.HIGH)

def left():
    GPIO.output(in2, GPIO.LOW)
    GPIO.output(in1, GPIO.LOW)
    GPIO.output(en1, GPIO.LOW)

def right():
    GPIO.output(in3, GPIO.LOW)
    GPIO.output(in4, GPIO.LOW)
    GPIO.output(en2, GPIO.LOW)

def stop():
    GPIO.output(en1,GPIO.LOW)
    GPIO.output(en2,GPIO.LOW)

#setup pwm
pwm_right = GPIO.PWM(en2, 100)
pwm_left = GPIO.PWM(en1, 100)

while True:
    cmd = raw_input("Command, w = forward / s = backward / x = stop (add a number between 0..9 for speed eg f6):")
    if len(cmd) > 0:
        direction = cmd[0]
    if direction == "w":
        forwards()
    elif direction == "s":
        backwards()
    elif direction == "a":
        left()
    elif direction == "d":
        right()
    elif direction == "x":
        stop()
    else:
        stop();

    speed = int(cmd[1]) * 11
    pwm_left.start(speed)
    pwm_right.start(speed)

GPIO.cleanup()