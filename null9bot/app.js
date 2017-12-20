var tmi = require('tmi.js');

/*if you change channel and bot Name, set this values*/
var channelName = "neolgu71";
var botName = "NULL9_BOT";
var botpassword = "oauth:t7e96i6xmws5318f9jqbjatfq9kpf9"
///////////////////////////////////////////////////////

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

/*client.action("channel name", "message"); <-- send massage to chat

/*get chatting message */
client.on('chat', function(channel, user, message, self) {
	/////////////instructing word///////////////
	if(message === "!봇아" && user.username === "neolgu71") {
		client.action(channelName, "네 주인님");
	}
	else if(message === "!봇아"){
		client.action(channelName, "너 주인님 아니야.");
	}
	
	if(message === "!투표방법"){
			client.action(channelName, "!투표키 투표번호/키워드 를 입력하세요");
	}
	
	if(message === "!명령어"){
			client.action(channelName, "!봇아	!투표방법	!투표키 투표번호/키워드");
	}
	
	/*chatting to vote*/
	if(message[0] == '!'){
		var vmsg = message.substring(1).split(" ");
		//vote
		if(votes[parseInt(vmsg[0])] != undefined && votes[parseInt(vmsg[0])] != null && !isNaN(vmsg)){
			votes[parseInt(vmsg[0])].voteList[parseInt(vmsg[1])] += 1;
			
			//client.action(channelName, votes[parseInt(vmsg[0])].voteList[parseInt(vmsg[1])]);
		}
		//keyword vote 
		if(votes[parseInt(vmsg[0])] != undefined && votes[parseInt(vmsg[0])] != null){
			client.action(channelName, parseInt(vmsg[0]) + "입력됨");
			if(socket_ != undefined){
				socket_.write("VOTEBAK_"+vmsg[0]);
			}
		}
	}
});

client.on('connected', function(address, port) {
	client.action(channelName, "널구의 하수인이 등록되었습니다.")
});


///////////////////////VOTE////////////////////////
function Vote(str){
	this.prikey;
	this.voteName;
	this.voteTime = parseFloat(str);
	this.list = [];//list text
	this.voteList = [];//number of vote
	this.isEnd = false; //vote is end
	
	//get vote result
	Vote.prototype.getResult = function(){
		var max=0;
		var maxIndex;
		
		for (temp in this.voteList){
			if(max < this.voteList[temp]){
				max = this.voteList[temp];
				maxIndex = temp;
			}
		}
		
		return maxIndex;
	}
	
	//get vote information
	Vote.prototype.getList = function(){
		var str = "#" + this.prikey + "	" +this.voteName.toString();
		var temp;
		for (temp in this.list){
			str += "	";
			str += temp + "." +this.list[temp].toString('utf8');
		}
		return str;
	}
}
//keyword vote
function KWVote(str){
	this.prikey;
	this.voteName;
	this.voteTime = parseFloat(str);
	this.keyword;
	
	//get vote information
	KWVote.prototype.getList = function(){
		var str = "#"+ this.prikey + " ";
		
		str += "	"+this.keyword+"를 입력하세요!!!";
		
		return str;
	}
}

// Vote object
var votes = [];
// vote timer
var timers = [];

//vote time over
function voteTimeOver(votepri, socket){
	var result = votes[votepri].getResult();//get result
	
	socket.write("VOTEEND_"+votepri+"_"+result);//send result to game
	console.log("VOTEEND_"+votepri+"_"+result);
	
	client.action(channelName, "#"+votepri+"  투표종료!");//chat vote is end.
	delete votes[votepri];
}
//keyword vote time over 
function keyTimeOver(votepri, socket){
	socket.write("VOTEEND_"+votepri);//send to game vote is end
	console.log("VOTEEND_"+votepri);
	
	client.action(channelName, "#"+votepri+"  투표종료!");//chat vote is end.
	delete votes[votepri];
}

//////////////////////Socket with game/////////////////
console.log('socket server run');

//make server socket
var net = require('net');

var socket_;

net.createServer(function (socket) {

	socket.on('data', function (data) {
		//console.log(data.toString('utf8'));//read from client
		///////////////////////////////// vote check;
		socket_ = socket;
		
		if(data != null)
			var sptdata = data.toString('utf8').split("_");
		
		//set vote and get primarykey, type, time
		if(sptdata != null && sptdata[0] === "VOTESET"){
			console.log('voteSET 옴');
			var pri = parseInt(sptdata[1]);
			
			if(parseInt(sptdata[2]) === 1){
				if(votes[pri] == undefined || votes[pri] == null){
					client.action(channelName, "새로운 투표가 설정중입니다");
					votes[pri] = new Vote(sptdata[3]);
					votes[pri].prikey = pri;
				}
			}
			else if(parseInt(sptdata[2]) === 2){
				if(votes[pri] == undefined || votes[pri] == null){
					client.action(channelName, "새로운 투표가 설정중입니다");
					votes[pri] = new KWVote(sptdata[3]);
					votes[pri].prikey = pri;
				}
			}
		}
		//set vote name
		if(sptdata != null && sptdata[0] === "VOTENM"){
			console.log('voteNM 옴');
			var pri = parseInt(sptdata[1]);
			
			if(votes[pri] != undefined){
				if(votes[pri].voteName == undefined ||votes[pri].voteName == null){
					votes[pri].voteName = sptdata[2];
				}
			}
		}
		//get and set vote list
		if(sptdata != null && sptdata[0] === "VOTELST"){
			console.log('voteLST 옴');
			var pri = parseInt(sptdata[1]);
			var lst = parseInt(sptdata[2]);
			
			if(votes[pri] != undefined){
				if(votes[pri].list[lst] == undefined || votes[pri].list[lst] == null){
					votes[pri].voteList[lst]=0;
					votes[pri].list[lst]=sptdata[3];
				}
			}
		}
		//set vote keyword
		if(sptdata != null && sptdata[0] === "VOTEKEY"){
			console.log('voteKEY 옴');
			var pri = parseInt(sptdata[1]);
			
			if(votes[pri] != undefined){
				if(votes[pri].keyword == undefined || votes[pri].keyword == null){
					votes[pri].keyword = sptdata[2];
				}
			}
		}
		//vote set end  and  vote start
		if(sptdata != null && sptdata[0] === "VOTELED"){
			console.log('voteLED 옴');
			var pri = parseInt(sptdata[1]);
			
			if(votes[pri] != undefined){
				client.action(channelName, "투표가 시작되었습니다.");//chat to channel vote start
				client.action(channelName, votes[pri].getList());//chat to what vote
			}
			//vote start until vote time
			var asd = setTimeout(voteTimeOver, votes[pri].voteTime * 1000, pri,socket);
			
			//clearTimeout(asd);
		}
		//keyword vote start
		if(sptdata != null && sptdata[0] === "VOTEKS"){
			console.log('voteKS 옴');
			var pri = parseInt(sptdata[1]);
			
			if(votes[pri] != undefined){
				client.action(channelName, "키워드 입력이 시작되었습니다.");//chat to channel keyword vote start
				client.action(channelName, votes[pri].getList());
			}
			//vote timer start
			var asd = setTimeout(keyTimeOver, votes[pri].voteTime * 1000, pri,socket);
			
			//clearTimeout(asd);
		}
		
		////////////////////////////////
	//socket.write(data);//send to client
});
}).listen(8000, function() {//open port 8000
console.log('TCP Server Running ~!'); 
});;


