// Initialize the Image Classifier method with MobileNet. A callback needs to be passed.
let classifier;

// A variable to hold the image we want to classify
let img;

function preload() {
  classifier = ml5.imageClassifier('MobileNet');
  img = loadImage('../images/1987643.jpg');
}

function setup() {
  createCanvas(800, 800 * img.height / img.width);
  classifier.classify(img, gotResult);
  image(img, 0, 0, 800, 800 * img.height / img.width);
}

// A function to run when we get any errors and the results
function gotResult(error, results) {
  // Display error in the console
  if (error) {
    console.error(error);
  } else {
    // The results are in an array ordered by confidence.
    console.log(results);
    for(var i = 0; i < 3; i++) {
        createDiv(`Label: ${results[i].label}`);
        createDiv(`Confidence: ${nf(results[i].confidence, 0, 2)}`);
    }
    
  }
}

