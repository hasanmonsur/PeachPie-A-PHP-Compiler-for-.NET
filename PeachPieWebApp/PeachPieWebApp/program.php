<?php

    // Simple greeting function
    function greet($name) {
        return "Hello, $name! from PHP";
    }

    // Return current server info
    function getServerInfo() {

       $php_array= [
            'php_version' => phpversion(),
            'server_software' => $_SERVER['SERVER_SOFTWARE'] ?? 'N/A',
            'request_time' => date('Y-m-d H:i:s')
        ];

        $json = json_encode($php_array);
        if (json_last_error() !== JSON_ERROR_NONE) {
            throw new Exception("JSON encode error: " . json_last_error_msg());
        }
        return $json;

      }


// Calculator class
class Calculator {
    public static function add($a, $b) {
        return $a + $b;
    }
    
    public static function multiply($a, $b) {
        return $a * $b;
    }
}