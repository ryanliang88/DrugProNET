<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrugInfo.aspx.cs" Inherits="DrugProNET.DrugInfo" %>

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
    <link rel="stylesheet" href="./css/drug_info.css">
    <link rel="stylesheet" href="./css/3_column.css">
    <script src="./js/option_select_redirect.js"></script>
    <title>DrugProNET | Drug Information</title>
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
                        <a href="index.html">Home</a>
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
                <h3 class="h3-side-title">Query Type</h3>
            </div>
            <div class="c-col body-content">
                <h3 class="h3-body-title">Drug Information</h3>
                <p>
                    This query provides detailed information on over 2000 compounds
                    that have been experimentally identified as inhibitors of one or
                    more human proteins. Follow the instructions below to retrieve
                    information on a specific drug of interest.
                </p>
            </div>
            <div class="c-col advertisment-content">
                <asp:HyperLink ID="ad_link" NavigateUrl="navigateurl" runat="server">
                    <asp:Image ImageUrl="imageUrl" runat="server" ID="ad_banner" AlternateText=""/>
                </asp:HyperLink>
            </div>
        </div>
        <div class="c-row">
            <div class="c-col side-content">
                <h3 class="h3-side-title">Filters</h3>
            </div>
            <div class="c-col body-content">
                <h3 class="h3-body-title">Step 1 - Drug Specification</h3>
                <p>
                    Enter the first few characters for a compound name, CAS ID,
                    PubChem ID or ChEMBL ID and then select the desired search
                    term from the Drop Down List.
                </p>
                <input class="button" type="button" value="Type in at least 3 letters of search term" />
                <h3 class="h3-body-title">Step 2 - Retrieve Compound Information</h3>
                <p>
                    Click on the buttons below to retrieve information on the
                    compound of interest or to reset the parameters for a new query.
                </p>
                <input class="button" type="button" value="Retrieve information">

                <!-- This is just used to space the button above and below this line apart -->
                <span>&emsp;&emsp;</span>

                <input class="button" type="button" value="Reset">
            </div>
        </div>

        <div class="c-row">
            <div class="c-col side-content"></div>
            <footer class="c-col body-content footer-content">
                <h2>Copyright © 2018 Kinexus Bioinformatics Corporation. All rights reserved.</h2>
            </footer>
        </div>
        <!-- This must be at bottom! -->
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
    </div>
</body>

</html>

