@echo off
echo TEST 1
DELFLS /? 
IF %ErrorLevel% == 0 echo TEST IS OK!
IF NOT %ErrorLevel% == 0 echo TEST IS NOT OK!
pause
cls
echo TEST 2
DELFLS /create
IF %ErrorLevel% == 0 echo TEST IS OK!
IF NOT %ErrorLevel% == 0 echo TEST IS NOT OK!
pause
cls
echo TEST 3
DELFLS test
IF %ErrorLevel% == 0 echo TEST IS OK!
IF NOT %ErrorLevel% == 0 echo TEST IS NOT OK!
pause
cls
echo TEST 4
DELFLS /allf
IF %ErrorLevel% == 0 echo TEST IS OK!
IF NOT %ErrorLevel% == 0 echo TEST IS NOT OK!
pause