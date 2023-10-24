# AIBilSpelRep

# 1. Installing python

## 1.1
Goto [https://www.python.org/downloads/release/python-3913/]([url](https://www.python.org/downloads/release/python-3913/)) and download the correct installer for your system (Windows installer 64-bit(recommended))

## 1.2
Check in "ADD PYTHON 3.9 TO PATH" when asked

# 2. Installing mlagents

## 2.1 
Go to the unity projects file path (e.g. C:\Users\YOUR_USER\Documents\GitHub\AIBilSpelRep\AiBilspelGithub)

## 2.2
Open a command promt in the folder by clicking the path typing "cmd" and clicking enter.

![Screenshot_107](https://github.com/TheSwe/AIBilSpelRep/assets/39467732/3d916d44-4305-4575-b4f2-f5771d24658f)

## 2.3 
Run the command "python" and check that the correct version is in use (3.9.13)

## 2.4
Run the command "exit()"

## 2.5 
Run the command "python -m venv venv"

## 2.6 
Run the command "venv\Scripts\activate"

## 2.7 Installing pip
Run the command "python -m pip install --upgrade pip"

## 2.8 Installing MLagents package
Run the command "pip install mlagents"

## 2.9 Installing pytorch package
Run the command "pip3 installtorch torchvision torchaudio"

## 2.10 Upgrading protobuf package
Run the command "pip install protobuf==3.20.3"

## 2.11 Install packaging package
Run the command "pip3 install packaging"

## 2.12 Checking installation success
Run the command "mlagents-learn --help"

The result should be mlagents config as seen below

![Screenshot (9)](https://github.com/TheSwe/AIBilSpelRep/assets/39467732/e82e7941-5742-421c-bb25-27b4ef98f7cd)

# 3. Running mlagents

## 3.1 Starting

Open up a cmd in the project folder and activate the activate.bat script by running the command "venv\Scripts\activate"

Be sure unity is open, then run the command "mlagents-learn --run-id=YOURID". The id is just a way to identfy the result of the simulation.

## 3.2 Stopping

ctrl + c will result in an error which stops the simulation


