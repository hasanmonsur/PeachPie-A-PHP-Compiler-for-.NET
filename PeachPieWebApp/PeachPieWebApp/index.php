<?php

/** The entry point of the program. */
require_once './class1.php';

$c = new Library\Class1;
echo $c->encode('Hello World!');


//require_once './program.php';
include 'program.php';