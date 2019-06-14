//L'ajout d'une classe autofocus donne le focus au controle au chargement de la page
//Exemple : @Html.EditorFor(model => model.Libelle, new { htmlAttributes = new { @class = "form-control autofocus" } })
$(function () { $('.autofocus').focus(); });
