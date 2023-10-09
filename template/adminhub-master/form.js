const addOptionContainer = document.getElementById("add-option-container");

// Fonction pour créer un nouvel élément "options" et l'insérer avant "add-option-container" dans le formulaire
function createNewOptionAboveAddOption() {
  const newOptionDiv = document.createElement("div");
  newOptionDiv.className = "options";
  const icon = document.createElement("i");
  icon.className = "fa fa-circle-o";
  const newInput = document.createElement("input");
  newInput.type = "text";
  newInput.className = "option";
  newInput.placeholder =
    "Option " + document.getElementsByClassName("options").length;
  newOptionDiv.appendChild(icon);
  newOptionDiv.appendChild(newInput);

  // Insérer le nouvel élément "options" avant "add-option-container" dans le formulaire
  addOptionContainer.parentNode.insertBefore(newOptionDiv, addOptionContainer);
  newInput.focus();
}

// Ajoutez un gestionnaire d'événements pour créer un nouvel élément "options" au-dessus de "add-option-container" lors du focus
const addOptionInput = document.getElementById("add-option");
addOptionInput.addEventListener("focus", createNewOptionAboveAddOption);

// Sélectionnez l'élément "add" par son ID
const addButton = document.getElementById("add");

// Sélectionnez l'élément "questions" par son ID
const questionsDiv = document.getElementById("questions");

// Fonction pour cloner le formulaire existant et l'ajouter à "questions"
function addNewQuestionForm() {
  // Clonez le formulaire existant
  const newForm = document.getElementById("newform").cloneNode(true);

  // Effacez les valeurs saisies dans les champs
  newForm.querySelector("#question").value = "";
  newForm.querySelectorAll(".option").forEach(function (option) {
    option.value = "";
  });

  // Ajoutez le formulaire cloné à "questions"
  questionsDiv.appendChild(newForm);
  newForm.scrollIntoView({ behavior: "smooth" });
}
  addButton.addEventListener("click", function () {
  // Récupérez la question
  const question = document.getElementById("question").value;

  // Récupérez les valeurs des options depuis le tableau
  const optionValues = options.map(function (option) {
    return option.value;
  });

  // Créez un objet contenant les données à envoyer au serveur
  const data = {
    question: question,
    options: optionValues,
  };
  

  // Convertissez l'objet JavaScript en chaîne JSON
  const jsonData = JSON.stringify(data);

  const xhr = new XMLHttpRequest();
  xhr.open("POST", "http://localhost:5285/Test/addQuestion", true); 
  xhr.setRequestHeader("Content-Type", "application/json"); 

  xhr.onreadystatechange = function () {
    if (xhr.readyState === 4) {
      if (xhr.status === 200) {
 
        addNewQuestionForm();
      } else {

      }
    }
  };

  xhr.send(jsonData);
});
