## Image processing app - Octree Color Quantization

## General description
It is a desktop application created with Windows Forms from .NET environment. Its functionality is to create an octree from imported image and use it to have a number of colors in exported image reduced. Thanks to that model of lossy compression, we can decrease the size of a data file with quite slight loss of quality.

## Octree
Octrees seems to be a very efficient data structure. They are just like binary trees but each node can have 8 subnodes instead of 2. Searching and inserting into such an octree is very fast. In case of having 8-bit color components it is possible to easily choose the child node according to bits on particular position in all 3 components. More details about them can be found there: https://www.cubic.org/docs/octree.htm.

## Menu options
- [ ] Generate Picture - load default image as imported image
- [ ] Import Image - load image from image file (.bmp, .jpg, .png)
- [ ] Export Image - save image into image file (.bmp, .jpg, .png)

## Quantization Options
- [ ] Linear - then set final number of colors and number of remaining steps
- [ ] Exponential - then set final number of colors and ratio of downscaling

## Octree Visualization
Located on the bottom of the window, is updated on each going to the next step of quantization.

## Images
![Image 1](Images/image_1_1.png)
![Image 1](Images/image_1_2.png)
![Image 1](Images/image_1_3.png)
![Image 2](Images/image_2_1.png)
![Image 2](Images/image_2_2.png)
![Image 2](Images/image_2_3.png)
![Image 2](Images/image_2_4.png)
