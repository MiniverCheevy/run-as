run-as
======

A command line wrapper for runas.exe.

This application stores application and account names (but not passwords) to allow an easier runas.exe experience.

Rather than

runas /u:UserName@SomeClient /netonly "c:\Program Files (x86)\Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE\ssms.exe"

you can use

ra SomeClient ssms

Put it on your path and you can launch it from the Run box.

The configuration system launched with

ra config

is a self hosted Angular/WebApi website hosted in the console app using OWIN.
