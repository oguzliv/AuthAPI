# AuthAPI
<h1>Authentication API</h1>
<h3>Summary</h3>
<p>This project provides login,registering, resetting password and verifying registeration with activation code.</p>

<h3>How to run</h3>
<ul>
  <li>First download postgresql and configure it according to your machine.</li>
  <li>After installation, save postgresql host, username and password data. appsettings.json has the configuration info, change it respectively.</li>
  <li>open Package Manager Console and write add-migration <migration-name> then update-database</li>
  <li>After getting no errors, you run the application and test the end points by using POSTMAN</li>
</ul>

<h3>Work Flow</h3>
<ul>
  <li> Assumed there is only one entity User.</li>
  <li> JWT is used for authentication. Roles also defined into JWT. </li>
  <li> When user hits the register endpoint. Randomly generated verification code is sent to user specified email.</li>
  <li> Resetting password is assumed as there is a page for resetting password. In request body, reqired data is stored</li>
  <li> Updating user pasword is different operation, hadnled differently.</li>
  <li> There are all CRUD operations for user. Only delete endpoint is authorized</li>
  <li> Admin operations also authenticated with JWT.</li>
  <li> There are any unit tests. All functionalities are tested manually.</li>
</ul>
