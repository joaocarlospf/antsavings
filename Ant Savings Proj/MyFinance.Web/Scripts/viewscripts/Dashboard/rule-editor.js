var ModuleDepositRules;

$(document).ready(function () {
    initializeRule();

    $('.mdl-deposit-rules select').selectpicker({ style: 'btn-primary', menuStyle: 'dropdown-inverse' });
    $('.mdl-deposit-rules #btnAdicionarRegra').click(adicionarRegra);
    $('.modal-footer #saveRule').click(postAndUpdateRule);
    $('#newRule').click(loadNewRule);
});

function initializeRule() {
    
    ModuleDepositRules = {};
    ModuleDepositRules.regras = [];
    ModuleDepositRules.nome = '';
    ModuleDepositRules.id = 0;
    
    $('.mdl-deposit-rules .rules-list').html('');

}

function loadNewRule() {
    initializeRule();

    $('#ruleId').val(ModuleDepositRules.id);
    $('#ruleName').val(ModuleDepositRules.nome);
    $('#addRuleModal').modal('show');
}

function postAndUpdateRule() {
    $('#addRuleModal').modal('hide');
    ModuleDepositRules.nome = $('#ruleName').val();
    ModuleDepositRules.id = $('#ruleId').val();
    $.ajax({
        url: "/Rules/SubmitRule",
        type: "POST",
        data: JSON.stringify(ModuleDepositRules),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            $('#dashboardContent').html('').append(data);
            adjustCss();
        }
    });
}

function getRule(url, id, placeToLoad) {
    $.post(url, id,
        function (resp) {
            adjustCss();
            initializeRule();
            loadRule(resp);
            $('#addRuleModal').modal('show');
        });
}

function loadRule(mdr) {
    ModuleDepositRules = mdr;
    $('#ruleId').val(mdr.id);
    $('#ruleName').val(mdr.nome);
    for (var i = 0; i < ModuleDepositRules.regras.length; i++) {
        adicionarRegraNoHTML(ModuleDepositRules.regras[i]);
    }
}

function removerRegra(itemThis) {
	var idRegra = $(itemThis).closest('.rules-item').attr('id').slice(5);
	var porcentagem = 0;
	var indexToRemove;

	for (var i=0; i < ModuleDepositRules.regras.length; i++) {
		if (ModuleDepositRules.regras[i].id == idRegra) {
			porcentagem = ModuleDepositRules.regras[i].porcentagem;
			indexToRemove = i;
		}
	}

	ModuleDepositRules.regras.splice(indexToRemove, 1);
	$('#rule-' + idRegra).remove();

	if (porcentagem > 0) {
		atualizarPorcentagem(porcentagem, idRegra);
	}
}

function adicionarRegra() {
	var regra = getRegraSelecionada();

	if (jaExisteRegra(regra.fundo.id, regra.objetivo.id)) {
	    mostrarMensagemDeErro('Esta combinação fundo-objetivo já existe! Escolha outra combinação.');
		return;
	}

	regra.id = new Date().getTime();
	regra.porcentagem = ModuleDepositRules.regras.length > 0 ? 0 : 100;
	regra.fixa = false;
	
	if (regra.porcentagem == 0) {
		regra.porcentagem = getTotalPorcentagemFaltante();
	}

	adicionarRegraNoHTML(regra);
	ModuleDepositRules.regras.push(regra);
}

function fixarRegra(itemThis) {
	var idRegra = $(itemThis).closest('.rules-item').attr('id').slice(5);
	for (var i=0; i < ModuleDepositRules.regras.length; i++) {
		if (ModuleDepositRules.regras[i].id == idRegra) {
			if ($('#chk-' + idRegra).hasClass('checked')) {
				ModuleDepositRules.regras[i].fixa  = true;
			}
			else ModuleDepositRules.regras[i].fixa  = false;
		}
	}
}

function getRegraSelecionada() {
	var regra = {};
	var fundo = {};
	var objetivo = {};

	fundo.id = $('.mdl-deposit-rules #fundos').val();
	fundo.text = $('.mdl-deposit-rules #fundos > span').text();

	objetivo.id = $('.mdl-deposit-rules #objetivos').val();
	objetivo.text = $('.mdl-deposit-rules #objetivos > span').text();

	regra.fundo = fundo;
	regra.objetivo = objetivo;

	return regra;
}

function adicionarRegraNoHTML(regra) {
	$('.mdl-deposit-rules .rules-list').append('<li id="rule-'+ regra.id +'" class="rules-item"><span class="fundo">'+ regra.fundo.text +'</span><span class="objetivo">'+ regra.objetivo.text +'</span><span id="chk-'+regra.id+'" class="checkbox" style="margin-top: 2px"><input type="checkbox" data-toggle="checkbox"></span><div class="ui-slider"></div><span class="porcentagem">'+ Number(regra.porcentagem).toFixed(2) +'%</span><span class="btn-remove fui-cross"></span></li>');
	
	$('input[type=checkbox]').checkbox();
	
	$('.mdl-deposit-rules #rule-' + regra.id).find('input[type=checkbox]').on("change", function() {
		fixarRegra(this);
	});
	$('.mdl-deposit-rules #rule-' + regra.id).find('.btn-remove').click(function() {
		removerRegra(this);
	});

	$('.mdl-deposit-rules #rule-' + regra.id).find('.ui-slider').slider({
		min: 0,
		max: 100,
		value: regra.porcentagem,
		orientation: "horizontal",
		range: "min",
		slide: function(event, ui) {
			if (ModuleDepositRules.regras.length == 1) {
				mostrarMensagemDeErro('Você deve adicionar outra combinação fundo-objetivo para poder alterar esta!');
				return false;
			}
			
			return onChangeSelect(this, event, ui);
		}
	});
}

function onChangeSelect(itemThis, event, ui) {
	var value = Number(ui.value);
	var idRegra = $(itemThis).closest('.rules-item').attr('id').slice(5);
	var antigaPorcentagem = getPorcentagem(idRegra);
	var porcentagem = antigaPorcentagem - value;
	
	ret = atualizarPorcentagem(porcentagem, idRegra);
	
	if (ret == true)
		$('#rule-' + idRegra).find('.porcentagem').text(Number(value).toFixed(2) + '%');
	
	return ret;
}

function getPorcentagem(idRegra) {
	for (var i=0; i<ModuleDepositRules.regras.length; i++) {
		if (ModuleDepositRules.regras[i].id == idRegra) {
			return ModuleDepositRules.regras[i].porcentagem;
		}
	}
}

function setPorcentagem(idRegra, novaPorcentagem) {
	for (var i=0; i<ModuleDepositRules.regras.length; i++) {
		if (ModuleDepositRules.regras[i].id == idRegra) {
			ModuleDepositRules.regras[i].porcentagem = novaPorcentagem;
		}
	}	
}


function atualizarPorcentagem(porcentagem, idRegraAIgnorar) {
	var somaTotalAtiva = 0, nrItems = 0, regra;
	
	for (var i=0; i<ModuleDepositRules.regras.length; i++) {
		regra = ModuleDepositRules.regras[i];
		if (regra.id != idRegraAIgnorar && regra.fixa == false) {
			somaTotalAtiva += regra.porcentagem;
			nrItems++;
		}
	}
	
	if (nrItems == 0 || somaTotalAtiva + porcentagem > 100 || somaTotalAtiva + porcentagem < 0) return false;
	
	for (var i=0; i<ModuleDepositRules.regras.length; i++) {
		regra = ModuleDepositRules.regras[i];

		if (regra.id != idRegraAIgnorar && regra.fixa == false) {
			if (somaTotalAtiva == 0) {
				regra.porcentagem += porcentagem / nrItems;
			} else {
				if (regra.porcentagem <= 100) {
					regra.porcentagem += porcentagem * (regra.porcentagem / somaTotalAtiva);
				}
			}
		} else if (regra.id == idRegraAIgnorar) {
			regra.porcentagem -= porcentagem;
		}

		if (regra.porcentagem < 0) regra.porcentagem = 0;

		atualizarPorcentagemNoHTML(regra.id, regra.porcentagem);
	}
	return true;
}

function atualizarPorcentagemNoHTML(idRegra, porcentagem) {
	porcentagem = Number(porcentagem).toFixed(2);
	$('#rule-' + idRegra).find('.porcentagem').text(porcentagem + '%');
	$('#rule-' + idRegra).find('.ui-slider').slider('value', porcentagem);
	$('#rule-' + idRegra).find('.ui-slider').focus().blur();
}

function isInt(n) {
   return n % 1 === 0;
}

function jaExisteRegra(idFundo, idObjetivo) {
	var existe = false;

	if (ModuleDepositRules.regras.length > 0) {
		for (var i=0; i<ModuleDepositRules.regras.length; i++) {
			if (ModuleDepositRules.regras[i].fundo.id == idFundo && ModuleDepositRules.regras[i].objetivo.id == idObjetivo) {
				existe = true;
				break;
			}
		}
	}

	return existe;
}

function mostrarMensagemDeErro(mensagem) {
    $('.mdl-deposit-rules .error-msg').text(mensagem).attr('style', 'display: inline-block');

    setTimeout(function () {
        $('.mdl-deposit-rules .error-msg').attr('style', 'display: none');
    }, 2000);
}

function getTotalPorcentagemFaltante() {
	var porcentagem = 0;

	for (var i=0; i<ModuleDepositRules.regras.length; i++) {
		porcentagem += Number(ModuleDepositRules.regras[i].porcentagem);
	}

	return 100-porcentagem;
}

function getRegrasJSONObject(encoded) {
	var json = JSON.stringify(ModuleDepositRules.regras);

	if (encoded) {
		var jsonEncoded = encodeURIComponent(json);
		return jsonEncoded;
	} else {
		return json;
	}
}