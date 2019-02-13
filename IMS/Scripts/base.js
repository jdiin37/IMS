if (typeof (Storage) !== "undefined") {
	//alert("支援Web Storage");
} else {
	alert("此瀏覽器不支援Web Storage");
}


function setWebStorage(key,value) {
	localStorage.setItem(key, value);//前面的window. 可省略不寫
	//localStorage.setItem("Key名稱", "字串值");//前面的window. 可省略不寫
	//localStorage["Key名稱"] = "字串值";//這寫法有cookie的fu
	//localStorage.Key名稱 = "字串值"; 
}

function getWebStorage(key) {
	var str = localStorage.getItem(key);
	//var str = localStorage["Key名稱"]
	//var str = localStorage.Key名稱;
	return str;
}

//ajax 設定
$.ajaxSetup({	
	statusCode: {
		401: function () {
			//window.location.href = "/LogOn/";
		}
	}
});

AddAntiForgeryToken = function (data) {
	data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
	return data;
};

function popupwindow(url, title, w, h) {
	var left = (screen.width / 2) - (w / 2);
	var top = (screen.height / 2) - (h / 2);
	return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}

function doAjax(url, data, callback) {
	$.ajax({
		url: url,
		type: "post",
		data: AddAntiForgeryToken(data),
		success: function (result) {
			callback(result);
		},
		error: function () {
			alert("Unauthorized");
		}
	});
}

function doAjax(url, data, callback, callback_1) {
	$.ajax({
		url: url,
		type: "post",
		data: AddAntiForgeryToken(data),
		success: function (result) {
			!!callback && callback(result);
		},
		error: function () {
			alert("Unauthorized");
			//window.location.href = "/Logon/";
		}
	}).done(!!callback_1 && callback_1);
}






var PigFarmId = "";

function setPigFarmId(val) {
	$.cookie("pigFarmId", val, { path: '/', expires: 365 });
	PigFarmId = val;
}

function getPigFarmId() {
	return $.cookie("pigFarmId"); 
}


jQuery(document).ready(function ($) {
	if ($.cookie("sysLang")) {
		$("#i18n").val($.cookie("sysLang"));
	}

	function getURLParameter(url, name) {
		return decodeURI(
			(RegExp(name + '=' + '(.+?)(&|$)').exec(url) || [, null])[1]
		);
	}

	$("#i18n").change(function () {

		$.cookie("sysLang", $(this).val(), { path: '/', expires: 365 });
		location.reload();
	});


	//if ($.cookie("pigFarmId")) {
	//	$("#PigFarm").val($.cookie("pigFarmId"));
	//} else {
	//	setPigFarmId($("#PigFarm").val());

	//}

	//$("#PigFarm").change(function () {
	//	//alert(document.location.origin);
	//	setPigFarmId($(this).val());
	//	location.href = document.location.origin + "/Management/Home"
	//});
	
	
});