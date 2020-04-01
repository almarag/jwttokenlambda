## **JWT Token Generator Lambda**

### v0.1

Author: Alejandro Martinez (almarag@gmail.com)

### About this project

This AWS Lambda project generate JWT tokens to be used for a Gateway API authorizer. Admin credentials are set into a SQLite database using Entity Framework to access it. 

**NOTE:** This is **NOT A PRODUCTION-READY CODE!!!** I've made this project merely as a fast-coding exercise and since this is basically functional, there are many things to do in order to can be used for production (like, i.e. NOT USING SQLite). I'm not responsible for what anyone using this code. My goal is just to show some basic concepts about how JWT works and how this can be used in a cloud environment like AWS. 

The code is extensible and also there are several parts that can be adapted in other environments outside AWS Lambda (like using as part of web api project and many other scenarios). Feel free to experiment and make improvements on your own.

