'use strict';
const tinify = require('tinify');

tinify.key = '';

// Obtenemos los argumentos proporcionados al script.
const args = process.argv.slice(2);

// Sobreescribe el archivo con su versión comprimida.
tinify.fromFile(args[0]).toFile(error => {
    if (error) throw error.message;
    return 0;
});