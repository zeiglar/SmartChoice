function InitialHome() {
    CheckCSVFile();
}

function AnalyseCSVFile(data) {
    $("div#status").append("Read CSV File->");

    if (data == 0) {
        //Start retrieving
        $("div#status").append("Retrieve->");
        //GetSuburbs();
        GetSettingsInfo();
    }
    else {
        //Return error
        $("div#status").append("Errors");
        PutError(data);
    }
}

function PutError(data) {
    switch (data) {
        case 33:
            $("div#result").text("Incorrect App Setting");
            break;
        case 34:
            $("div#result").text("Cannot Find CSV File");
            break;
        case 35:
            $("div#result").text("Cannot Create CSV File");
            break;
        case 36:
            $("div#result").text("CSV File Has No Content");
            break;
        default:
            $("div#result").text("Cannot Create/Read CSV File.");
            break;
    }
}

function FillSettingsInfo(logs) {

    $('input#inpDeposit').val(logs.Desposit);
    $('input#inpMortgagRate').val(logs.MortgageRate);
    $('input#inpStrate').val(logs.Strate);
    $('input#inpWater').val(logs.Water);
    $('input#inpCouncil').val(logs.CouncilRate);
    $('input#inpRental').val(logs.Rental);
    $('input#inpAgentCommission').val(logs.AgentCommission);
    $('input#inpLettingFee').val(logs.LettingFee);

    FillSuburbsInfo(logs.Suburbs);
}

function FillSuburbsInfo(logs) {
    var content = $("div#results");
    content.empty();

    $.each(logs, function (index, log) {
        $("<div />", {
            html: $('<span />', {
                html: log.Name
            }).attr("class", "suburbName").after($('<span />', {
                html: log.State
            }).attr("class", "state")).after($('<span />', {
                html: log.Postcode
            }).attr("class", "postcode"))
        }).attr("id", log.Postcode).attr("class", "suburb")
            .click(function () { GetProperties(log.Postcode, log.Name, log.State, log.Postcode); }).appendTo(content);
        $('<div />', {}).attr("class", "clearer").appendTo(content);

        //list Properties
        $('<div />', {}).attr("id", "p" + log.Postcode).attr("class", "properties").appendTo(content);
        $('<div />', {}).attr("class", "clearer").appendTo(content);

        //Pop up - tax
    });
}

function FillPropertiesInfo(id, logs) {
    var content = $('div#p' + id + '.properties');
    content.empty();
    var id = 1;
    $.each(logs, function (index, log) {
        var priceId = "priceId_" + id;
        $("<div />", {
            html: $("<span />", {
                html: "Property:"
            }).attr("class", "propertyTypeText").after($('<span />', {
                html: log.PropertyType
            }).attr("class", "propertyTypeValue")).after($('<span />', {
                html: log.Price
            }).attr("class", "priceValue").attr("id", priceId)).after($('<span />', {
                html: "Bedroom:"
            }).attr("class", "bedroomText")).after($('<span />', {
                html: log.Bedrooms
            }).attr("class", "bedroomValue")).after($('<span />', {
                html: "Bathrooms:"
            }).attr("class", "bathroomText")).after($('<span />', {
                html: log.Bathrooms
            }).attr("class", "bathroomValue")).after($('<span />', {
                html: "Parkings:"
            }).attr("class", "parkingText")).after($('<span />', {
                html: log.Parkings
            }).attr("class", "parkingValue")).after($('<br />', {
            })).after($('<span />', {
                html: $("<a />", {
                    html: log.Location
                }).attr("class", "locationHyperlink").attr("href", log.HyperUri).attr("target", "_blank")
            }).attr("class", "locationValue"))
        }).attr("class", "property").click(function () {
            DisplayPopupContent(priceId);
        }).appendTo(content);

        $('<div />', {}).attr("class", "clearer").appendTo(content);

        id++;
    });
}

/* Select Tag */
function InitialSelectTag() {
    var tag = $("select");
    var html = '<option value="-1" selected>Any</option>\n"';
    for (var i = 50000; i <= 10000000; i += 50000) {
        html += '<option value="' + i + '">' + i + '</option>\n"'
    }

    tag.html(html);
}

/*****************************************************************************************************/
/***************************************** Pop up content ********************************************/
/*****************************************************************************************************/
//Designed for 2013
function GetTaxForTaxableIncome(income) {
    if (income < 18200)
        return 0;
    else if (18200 < income < 37000)
        return (income - 18200) * 0.19;
    else if (37001 < income < 80000)
        return (income - 37000) * 0.325 + 3572;
    else if (80001 < income < 180000)
        return (income - 80000) * 0.37 + 17547;
    else
        return (income - 180000) * 0.45 + 54547;
}

function DisplayPopupContent(id) {
    ClearPopupContentTotalValue();

    $("div.popupContent").fadeIn("slow");
    $("div#results").fadeOut("slow");

    //Find the price
    var prices = $("span#" + id).text().split(" ");
    var price = "0";
    for (var i = 0, len = prices.length; i < len; i++) {
        if (prices[i].substring(0, 1) === "$") {
            price = prices[i].replace("$", "").replace(",", "");
            break;
        }
    }
    //Set Property Price
    $("input#inpPropertyPrice").val(price);
    $("input#inpBuildingDepreciation").val(price * 0.025);
}

function ClearPopupContentTotalValue() {
    $('input.inputTotalValue').val(0);
    $('input.inpCashProfit').val(0);

}

//Calculate Yield
function CalculateYield() {
    //Get Mortage 
    var price = $('input#inpPropertyPrice').val();
    var deposit = $('input#inpDeposit').val();
    var mortgage = price - deposit;
    if (mortgage < 0) mortgage = 0;
    $('input#inpMortgage').val(mortgage);

    //Yearly Cost
    var interestRate = $('input#inpMortgagRate').val() / 1.00;
    var strate = $('input#inpStrate').val() / 1.00;
    var water = $('input#inpWater').val() / 1.00;
    var council = $('input#inpCouncil').val() / 1.00;
    var yearlyCost = mortgage * interestRate * 0.01 + 4 * (strate + water + council);
    $('input#inpYearlyCost').val(yearlyCost);

    //Rental Income
    var rentail = $('input#inpRental').val() / 1.00;
    var commission = $('input#inpAgentCommission').val() / 1.00;
    var lettingFee = $('input#inpLettingFee').val() / 1.00;
    var rentalIncome = rentail * 52.25;
    rentalIncome = rentalIncome + commission * rentalIncome * 0.01 + lettingFee;
    $('input#inpTotalRentalIncome').val(rentalIncome);

    //Annual Yield and Others
    var cashProfit = rentalIncome - yearlyCost;
    var buildingDepreciation = $("input#inpBuildingDepreciation").val() / 1.00;
    var nonCashProfit = buildingDepreciation - cashProfit;
    var taxBenefit = nonCashProfit * 0.0385;
    var totalBenefit = taxBenefit - cashProfit;
    var annualYield = 0;
    if (price != 0) {
        annualYield = Math.abs(totalBenefit / price) * 100;
    }
    $('input#inpCashProfit').val(cashProfit);
    $('input#inpNonCashProfit').val(nonCashProfit);
    $('input#inpTaxBenefit').val(taxBenefit);
    $('input#inpTotalBenefit').val(totalBenefit);
    $('input#inpAnnualYield').val(annualYield);
}