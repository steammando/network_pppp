var tmi = require('tmi.js');

var channelName = "neolgu71";
var botName = "NULL9_BOT";
var botpassword = "oauth:t7e96i6xmws5318f9jqbjatfq9kpf9"

var options = {
	options: {
		debug: true
	},
	connection: {
		cluster: "aws",
		reconnect: true
	},
	identity: {
		username: botName,
		password: botpassword
	},
	channels: [channelName]
};

var client = new tmi.client(options);
client.connect();

/*client.on("channel name", "message"); <-- send massage

/*get chatting message */
client.on('chat', function(channel, user, message, self) {
	if(message === "!노예야") {
		client.action(channelName, "네 주인님");
	}
	
	if(message[0] == '!'){
		
		
		if(message === "!투표방법"){
			client.action(channelName, "!투표키 투표번호 룰 입력하세요");
		}
	}
});

client.on('connected', function(address, port) {
	client.action(channelName, "전용 노예 입장 데수웅")
});


///////////////////////VOTE////////////////////////
function Vote(str){
	this.prikey;
	//this.vtype = parseInt(sptstr[2]);
	this.voteName;
	this.voteTime = parseFloat(str);
	this.list = [];//list text
	this.voteList = [];//number of vote
	this.isEnd = false; //vote is end
	
	//get vote result
	Vote.prototype.getResult = function(){
		var max=0;
		var maxIndex;
		
		for(var i = 0; m = this.List.length; i+=1){
			if(this.voteList[i] > max){
				max = this.voteList[i];
				maxIndex = i;
			}
		}
		
		return maxIndex;
	}
	
	Vote.prototype.getList = function(){
		var str = this.voteName.toString();
		var temp;
		for (temp in this.list){
			str += "	";
			str += temp + ". " +this.list[temp].toString('utf8');
		}
		return str;
	}
}

var votes = [];
var votenum = 0;

//////////////////////Socket with game/////////////////
console.log('socket server run');

/*
var app = require('http').createServer(handler)
var fs = require('fs');

app.listen(8000);// port 8000

function handler (req, res) {
  fs.readFile(__dirname + '/index.html',
  function (err, data) {
      if (err) {
          res.writeHead(500);
          return res.end('Error loading index.html');
      }

      res.writeHead(200);
      res.end(data);
  });
}

// socket.io 스타트
var io = require('socket.io')(app);

// 클라이언트 컨넥션 이벤트 처리
io.on('connection', function (socket) {

    // 'news' 이벤트 send
    socket.emit('news', { hello: 'world' });

    // 'my other event' 이벤트 receive
    socket.on('my other event', function(data) {
        console.log(data);
    });

});
*/

 var net = require('net');

net.createServer(function (socket) {

	socket.on('data', function (data) {
		//console.log(data.toString('utf8'));//read from client
		///////////////////////////////// vote check;
		if(data != null)
			var sptdata = data.toString('utf8').split("_");
		
		//투표 생성
		if(sptdata != null && sptdata[0] === "VOTESET"){
			console.log('voteSET 옴');
			var pri = parseInt(sptdata[1]);
			
			client.action(channelName, "새로운 투표가 설정중입니다");
			
			if(parseInt(sptdata[2]) === 1){
				votes[pri] = new Vote(sptdata[3]);
				votes[pri].prikey = pri;
			}
		}
		//투표 이름 설정
		if(sptdata != null && sptdata[0] === "VOTENM"){
			console.log('voteNM 옴');
			var pri = parseInt(sptdata[1]);
			
			//client.action(channelName, "새로운 투표가 등록되었습니다.");
			
			if(votes[parseInt(sptdata[1])] != undefined){
				console.log(sptdata[2]);
				votes[parseInt(sptdata[1])].voteName = sptdata[2];
			}
		}
		//투표 리스트 받기
		if(sptdata != null && sptdata[0] === "VOTELST"){
			console.log('voteLST 옴');
			var pri = parseInt(sptdata[1]);
			var lst = parseInt(sptdata[2]);
			
			//client.action(channelName, "새로운 투표가 등록되었습니다.");
			
			if(votes[pri] != undefined){
				votes[pri].voteList[lst]=0;
				votes[pri].list[lst]=sptdata[3];
			}
		}
		//투표리스트 끝 투표 시작
		if(sptdata != null && sptdata[0] === "VOTELED"){
			console.log('voteLED 옴');
			var pri = parseInt(sptdata[1]);
			
			//client.action(channelName, "새로운 투표가 등록되었습니다.");
			
			if(votes[pri] != undefined){
				client.action(channelName, "투표가 시작되었습니다.");
				client.action(channelName, votes[pri].getList());
			}
		}
		
		
		//console.log(sptdata);
		////////////////////////////////
	socket.write(data);//send to client
});
}).listen(8000, function() {
console.log('TCP Server Running ~!'); 
});;


