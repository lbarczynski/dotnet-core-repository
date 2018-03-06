#!/usr/bin/python
import argparse
import os
import subprocess
from subprocess import call
from enum import Enum
import logging

logging.basicConfig(format='%(asctime)s - %(levelname)s - %(message)s', level=logging.DEBUG)
QUIET_MODE = False

def echo(text):
    if QUIET_MODE == False:
        logging.debug(text)

def call_command(command):
    call(command, shell=True, executable='/bin/bash')

def get_execution_dir():
    execution_dir = os.path.dirname(os.path.realpath(__file__)).split('/')
    del execution_dir[-1]
    return "/".join(execution_dir)

def call_dotnet_command(path, parameters, wait=True):
    execution_dir = os.getcwd() + path
    echo("Run command \'dotnet " + parameters + "\' in path: " + execution_dir)
    p = subprocess.Popen('dotnet ' + parameters, cwd=execution_dir, shell=True)
    if wait:
        try:
            p.wait()
        except KeyboardInterrupt:
            echo('Application closed with keyboard interrupt')

class BAPPS:
    def restore(self):
        call_dotnet_command('/src', 'restore')

    def clean(self):
        call_dotnet_command('/src', 'clean --configuration Debug')
        call_dotnet_command('/src', 'clean --configuration Release')

    def build(self):
        call_dotnet_command('/src', 'build --no-restore --configuration Release')

    def publish(self):
        call_dotnet_command('/src/BAPPS.EntityFrameworkRepository', 'publish --no-restore --configuration Release')

    def test(self):
        call_dotnet_command('/src/BAPPS.EntityFrameworkRepository.Tests', 'test --no-restore --no-build --configuration Release')

    def rebuild(self):
        self.clean()
        self.restore()
        self.build()    

    def full(self):
        self.rebuild()
        self.publish()
        self.test()

    def process_command(self, cmd):
        if cmd == CMD.restore:
            self.restore()
        if cmd == CMD.build:
            self.build()
        if cmd == CMD.rebuild:
            self.rebuild()
        if cmd == CMD.test:
            self.test()
        if cmd == CMD.publish:
            self.publish()
        if cmd == CMD.clean:
            self.clean()
        if cmd == CMD.full:
            self.full()

class CMD(Enum):
    clean = 'clean'
    restore = 'restore'
    build = 'build'
    rebuild = 'rebuild'
    publish = 'publish'
    test = 'test'
    full = 'full'

    def __str__(self):
        return self.value

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='BAPPS - Entity Framework Repository - Command Line Interface Tools')
    parser.add_argument('command', type=CMD, choices=list(CMD))
    parser.add_argument('-q','--quiet', help="Don\'t print any messages to stdout", action='store_true')
    parser.add_help
    args = parser.parse_args()   
    QUIET_MODE = args.quiet

    _BAPPS = BAPPS()
    _BAPPS.process_command(args.command)
