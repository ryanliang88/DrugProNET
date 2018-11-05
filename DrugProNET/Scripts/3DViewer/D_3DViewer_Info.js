var u = new NGL.Stage("D_3DViewer_viewport", {
    backgroundColor: "white",
    clipDist: 1
});

function loadDrugLigandInfo(fileName) {

    var loadFileUrl = "//files.rcsb.org//ligands/view/" + fileName + ".cif";

    u.loadFile(loadFileUrl).then(function (component) {
        component.setSelection("not _h and /0"),
            component.addRepresentation("ball+stick", {
                multipleBond: "symmetric"
            }),
            u.autoView(1)
    });
}