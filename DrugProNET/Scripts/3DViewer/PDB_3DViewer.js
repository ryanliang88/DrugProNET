var stage = new NGL.Stage("viewport", { // MUST BE NAMED "viewport" in ngl for unknown reasons
    backgroundColor: "white",
    clipDist: 1
});

function updateViewer(id) {
    loadStage(id);
}

function loadStage(val) {

    stage.loadFile("rcsb://" + val).then(function (o) {
        o.addRepresentation("cartoon", {
            sele: "*"
        })
        stage.autoView();
    });
}
