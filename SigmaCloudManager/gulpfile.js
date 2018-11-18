// Gulp configuration file

var gulp = require("gulp"),
  fs = require("fs"),
  less = require("gulp-less"),
  sass = require("gulp-sass");

// Create a runner for pre-processing the breadcrumb sass file to css
// and places the css file in the wwwroot/css folder
gulp.task("sass", function () {
  return gulp.src('Styles/breadcrumb.scss')
    .pipe(sass())
    .pipe(gulp.dest('wwwroot/css'));
});