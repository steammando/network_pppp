var net = require('net');

net.createServer(function (socket) {
socket.on('data', function (data) {
console.log(data.toString('utf8'));
socket.write(data);
});
}).listen(3000, function() {
console.log('_TCP SERVER_'); 
});;