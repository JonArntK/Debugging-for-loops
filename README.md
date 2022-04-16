# Debugging-for-loops
Made to help with debugging for loops in HLSL compute shaders

The example script (compute shader) found in this Unity-project is currently computed using only floats. However, I have found the difference to be even more noticable when using doubles. The reason double is not used here is to remove the possibility that the errors are due to convertion between the scalar types.

If you would like to try the code with double types instead, do a search-and-replace in both scripts "ComputeShader" and "Debugging" from "double" to "float". You may also have to change the hard-coded values of my results found in "Debugging" (which then are no longer valid) from float values to double (this is done by removing the "f" at the end).
