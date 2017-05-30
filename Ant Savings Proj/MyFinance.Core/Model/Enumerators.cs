using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Core.Model
{
    public enum EOperationType
    {
        [Description("Deposit")]
        Deposit = 0,
        [Description("Withdraw")]
        Withdraw = 1,
        [Description("Balance Update")]
        BalanceUpdate = 2
    }

    public enum ETimeUnit
    {
        [Description("EM MESES")]
        MONTH = 0,
        [Description("EM ANOS")]
        YEAR = 1
    }

    public enum EFundType
    {
        [Description("Poupança")]
        Poupanca = 0,
        [Description("CDI")]
        CDI = 1,
        [Description("Tesouro Direto")]
        TesouroDireto = 2,
        [Description("Fundo de Ações")]
        FundoAcoes = 3,
        [Description("Ações")]
        Acoes = 4,
        [Description("Outro")]
        Outro = 5
    }
}
