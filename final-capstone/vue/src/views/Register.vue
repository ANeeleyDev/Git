<template>
  <div id="register" class="text-center">
    <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
    <v-form v-model="valid" @submit.prevent="register">
      <v-container>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.username"
              :counter="10"
              label="Username"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.password"
              label="Password"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.confirmPassword"
              label="Confirm Password"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.firstName"
              label="First Name"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.lastName"
              label="Last Name"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.emailAddress"
              label="Email Address"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.phoneNumber"
              label="Phone Number"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="user.streetAddress"
              label="Street Address"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-select
              :items="city"
              label="City"
              v-model="user.city"
            ></v-select>
          </v-col>
          <v-col cols="12" md="4">
            <v-select
              :items="state"
              label="State"
              v-model="user.state"
            ></v-select>
          </v-col>
          <v-col cols="12" md="4">
            <v-select
              :items="zip"
              label="Zip Code"
              v-model="user.zip"
            ></v-select>
          </v-col>
        </v-row>
         <router-link :to="{ name: 'login' }">Have an account? </router-link>
      <v-btn type="submit">
        Create Account
      </v-btn>
      </v-container>
    </v-form>
    <!-- <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <label for="username" class="sr-only">Username</label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      <router-link :to="{ name: 'login' }">Have an account?</router-link>
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </form> -->
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "register",
  data() {
    return {
       city: [
        {
          text: "Cincinnati",
          value: 0,
        },
        {
          text: "Columbus",
          value: 1,
        },
         {
          text: "Toledo",
          value: 3,
        },
         {
          text: "Cleveland",
          value: 4,
        },
         {
          text: "Dayton",
          value: 5,
        },
      ],
      state: [
        {
          text: "Ohio",
          value: 34,
        },
      ],
      zip: [
        {
          text: "45249",
          value: 0,
        },
        {
          text: "41073",
          value: 1,
        },
         {
          text: "45201",
          value: 3,
        },
         {
          text: "45222",
          value: 4,
        },
         {
          text: "45237",
          value: 5,
        },
      ],
      
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
        firstName: "",
        lastName: "",
        emailAddress: "",
        phoneNumber: "",
        streetAddress: "",
        city: "",
        state: "",
        zip: "",
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style></style>
