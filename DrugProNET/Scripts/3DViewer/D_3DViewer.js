var u = new NGL.Stage("D_3DViewer_viewport", {
    backgroundColor: "white",
    clipDist: 1
});

/*
Author: Andy Tang
*/
function loadDrugLigand(fileName) {

    var loadFileUrl = "//files.rcsb.org//ligands/view/" + fileName + ".cif";

    u.loadFile(loadFileUrl).then(function (component) {
        component.setSelection("not _h and /0"),
            component.addRepresentation("ball+stick", {
                multipleBond: "symmetric"
            }),
            component.addRepresentation("label", {
                labelType: "atomname",
                color: "black",
                fontWeight: "bold ",
                xOffset: .1,
                yOffset: .1,
                radius: 1.4
            }),
            u.setSpin(true),
            u.autoView(1)
    });
}