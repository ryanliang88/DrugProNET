var stage = new NGL.Stage("viewport", { // MUST BE NAMED "viewport" in ngl for unknown reasons
    backgroundColor: "white",
    clipDist: 1
});

function loadStage(pdb, drug) {
    console.log(pdb + " " + drug);

    stage.loadFile("rcsb://" + pdb).then(function (component) {
        component.setSelection("not _h and /0"),
            component.addRepresentation("cartoon", {
                color: "atomindex"
            }),
            stage.autoView()
    });

    stage.loadFile("//files.rcsb.org//ligands/view/" + drug + ".cif").then(function (component) {
        component.setSelection("not _h and /0"),
            component.addRepresentation("ball+stick", {
                aspectRatio: 2,
                radius: .2
            }),
            stage.autoView();
    });
}
