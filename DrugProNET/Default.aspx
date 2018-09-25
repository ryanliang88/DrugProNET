<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DrugProNET.Default" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">

    <link rel="icon" href="./images/icon.png">

    <!-- Bootstrap CDNs -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="./css/base_style.css">
    <script src="./js/option_select_redirect.js"></script>

    <title>DrugProNET | Home</title>
</head>

<body>
    <div class="main-container">
        <div id="website-banner" class="banner">
            <img id="banner-image" src="./images/DrugProNETBanner.jpg" alt="Unable to display image" usemap="#banner-map">
            <map name="banner-map">
                <area shape="rect" coords="0, 0, 1024, 90" href="index.html" alt="Error">
                <area shape="rect" coords="0,57.2, 655, 147.2 " href="index.html" alt="Error">
                <area shape="rect" coords="655, 57.2, 1024, 147.2" href="http://www.kinexus.ca" target="_blank" alt="Error">
            </map>
        </div>

        <nav>
            <ul class="navigation-links horizontal-list">
                <li><a id="1" href="http://kinatlas.ca:8080" target="_blank">KinATLAS</a></li>
                <li><a id="2" href="http://www.transcriptonet.ca" target="_blank">TranscriptoNET</a></li>
                <li><a id="3" href="http://www.phosphonet.ca" target="_blank">PhosphoNET</a></li>
                <li><a id="4" href="http://www.onconet.ca" target="_blank">OncoNET</a></li>
                <li><a id="5" href="http://www.kinasenet.ca" target="_blank">KinaseNET</a></li>
                <li><a id="6" href="http://www.drugkinet.ca" target="_blank">DrugKiNET</a></li>
                <li><a id="7" href="http://www.kinet-am.ca" target="_blank">KiNET-AM</a></li>
                <li><a id="8" href="http://www.kinexus.ca/kinetica" target="_blank">Kinetica Online</a></li>
            </ul>
        </nav>
        <div class="col-container">
            <ul class="query horizontal-list col">
                <li>
                    <p>Select type of query desired</p>
                </li>
                <li>
                    <select id="query-selector">
                        <option value="">Hover to view options</option>
                        <option value="ProteinDrugQuery.html">Protein-focused Drug Interaction</option>
                        <option value="DrugProteinQuery.html">Drug-focused Protein Interaction</option>
                        <option value="ProteinInfo.html">Protein Information</option>
                        <option value="DrugInfo.html">Drug Information</option>
                    </select>
                </li>
            </ul>
            <ul class="secondary-navigation horizontal-list col">
                <div class="float-left">
                    <li>
                        <p>Updated September 2018</p>
                    </li>
                </div>
                <div>
                    <li>
                        <a href="">Home</a>
                    </li>
                    <li>
                        <p>|</p>
                    </li>
                    <li>
                        <a href="">Kinexus</a>
                    </li>
                    <li>
                        <p>|</p>
                    </li>
                    <li>
                        <a href="">Contact</a>
                    </li>
                    <li>
                        <p>|</p>
                    </li>
                    <li>
                        <a href="">Credits</a>
                    </li>
                </div>
            </ul>
        </div>
        <div class="c-row">
            <div class="c-col side-content">
                <div>
                    <ul class="left-sidebar-links">
                        <li>
                            <a href="index.html">Home</a>
                        </li>
                        <li>
                            <p>|</p>
                        </li>
                        <li>
                            <a href="#website-banner">Top</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="c-col body-content">
                <h2>About DrugProNET...</h2>
                <p>
                    DrugProNET is an open-access, knowledgebase developed by Kinexus Bioinformatics Corporation to
                    foster
                    the identification of the most critical atomic interactions between drugs and their protein targets
                    based on 3D x-ray crystallographic analyses. Defining the key amino acid residues for drug binding
                    in
                    proteins permits the prediction of specific mutations in human genomes that will affect the
                    sensitivities of individuals to these compounds. The bond distances in Angstroms between the
                    closest
                    protein and drug atoms in each crystal complex are provided in downloadable tables, along with
                    definition of the closest amino acid residue side-chains. DrugProNET features comprehensive
                    information
                    on over 2000 compounds that have been co-crystallized with over 480 different human proteins in
                    over
                    4400 protein-compound structures retrieved from the Research Collaboratory for Structural
                    Bioinformatics (RCSB) Protein Databank (PDB). In addition, information is also available for an
                    additional 748 compounds that are inhibitors of protein kinases. The protein and drug data were
                    retrieved from several sources, including the RCSB PDB, Wikipedia, the Universal Protein Resource
                    (UniProt), the National Center for Biotechnology Information’s (NCBI) RefSeq and PubChem websites,
                    and
                    the European Molecular Biology Laboratory (EMBL) European Bioinformatics Institute’s Kinase SARFari
                    and
                    ChEMBL websites.
                </p>
                <h2>Instructions</h2>
                <p>
                    DrugProNET is designed to be fast and simple to navigate. Just follow the steps outlined in each
                    query.
                    Presently, there are four different types of queries that you can perform with DrugProNET. You can
                    search for target proteins if you know their UniProt ID, RefSeq ID, or one of their common gene or
                    protein names. You can also query for approved drugs and other compound of interest if you know
                    their
                    CAS ID, PubChem CID, ChEMBL ID or one of their common or chemical names. A list of possible options
                    for
                    proteins or compounds are generated by typing at least two letters of their names or identification
                    numbers, and waiting for a few moments for a complete list to appear.
                </p>
                <h2>Other Useful Onlnie Resources From Kinexus...</h2>
                <p>
                    DrugProNET is one of several useful open-access, online resources that are available from Kinexus
                    to
                    foster cell signalling research by academic and industrial scientists. Direct links to several of
                    our
                    other knowledgebases in the SigNET KnowledgeBank are provided just below the header at the top of
                    each
                    webpage in DrugProNET. Our related DrugKiNET knowledgebase features comprehensive information on
                    over
                    800 compounds that have been experimentally determined to inhibit human protein kinases. This
                    includes
                    the retrieval of the lowest reported compound IC50, Ki and Kd values from several reputable sources
                    for
                    over 105,000 experimentally tested, non-redundant kinase-compound pairs. Using this information for
                    training, we developed two kinase inhibitor prediction algorithms to further predict another
                    200,000
                    possible kinase-compound interactions. TranscriptoNET provides gene expression information on over
                    20,300 human genes in over 600 diverse human cell lines, organs and tissues. PhosphoNET features
                    data
                    on nearly 1 million confirmed or predicted human phosphorylation sites in the proteins encoded by
                    these
                    genes. KinATLAS reveals interactions between these proteins with each other in signalling systems
                    as
                    well as interactions with kinase inhibitory compounds. KinaseNET offers detailed information about
                    536
                    human protein kinases, whereas OncoNET provides expression and mutation information for 3000
                    cancer-related proteins in diverse human tissues and cell lines. For semi-quantitative data on
                    actual
                    protein expression in over 2,000 diverse biological specimens, please also visit our open-access
                    KiNET
                    DataBank with over 2000 antibody microarray analyses with over 1700 diverse panand
                    phosphosite-specific
                    antibodies. Most of these antibodies are available from Kinexus for screening lysates from cells
                    and
                    tissues using our Kinex™ KAM antibody microarrays or as stand-alone products at our
                    www.kinexusproducts.ca website.
                </p>
            </div>
        </div>
        <div class="c-row">
            <div class="c-col side-content"></div>
            <footer class="c-col body-content footer-content">
                <h2>Copyright © 2018 Kinexus Bioinformatics Corporation. All rights reserved.</h2>
            </footer>
        </div>
    </div>
</body>

</html>

