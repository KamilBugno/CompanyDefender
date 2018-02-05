var persons = @JavaScriptConvert.SerializeObjectToJson(Model.PersonsGraph);
    var nodes = new vis.DataSet(persons);

    var mails = @JavaScriptConvert.SerializeObjectToJson(Model.MailsGraph);
    var edges = new vis.DataSet(mails);

    var container = document.getElementById('mynetwork');
    var data = {
        nodes: nodes,
        edges: edges
    };
    var options = {};
    var network = new vis.Network(container, data, options);
