<template>
  <div id="register" class="text-center">
    <h1
      v-if="this.$store.state.token === ''"
      class="h3 mb-3 font-weight-normal"
    >
      Create Account
    </h1>
    <h1 v-else class="h3 mb-3 font-weight-normal">Edit Account</h1>
    <v-form @submit.prevent="register">
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <v-container>
        <v-container v-if="this.$store.state.token === ''">
          <v-row>
            <v-col cols="12" md="4">
              <v-text-field
                v-model="user.username"
                :counter="50"
                label="Username"
                required
              ></v-text-field>
            </v-col>
            <!-- Changed counter from 10 to 50 by Kevin 12/14/2021 -->

            <v-col cols="12" md="4">
              <v-text-field
                v-model="user.password"
                label="Password"
                type="password"
                required
              ></v-text-field>
            </v-col>

            <v-col cols="12" md="4">
              <v-text-field
                v-model="user.confirmPassword"
                label="Confirm Password"
                type="password"
                required
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
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
            <v-autocomplete :items="city" label="City" item-text="cityName" item-value="cityId" v-model="user.city"></v-autocomplete>
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

        <v-container v-if="this.$store.state.token === ''">
          <v-btn type="submit"> Create </v-btn>
          <v-container
            ><router-link :to="{ name: 'login' }"
              >Have an account?
            </router-link>
          </v-container>
        </v-container>

        <v-container v-else>
          <v-btn type="submit"> Edit </v-btn>

          <v-btn v-on:click.prevent="cancelForm" type="cancel">Cancel</v-btn>
        </v-container>
      </v-container>
    </v-form>
  </div>
</template>

<script>
import authService from "../services/AuthService";
import userService from "../services/UserService";

export default {
  name: "register",
  data() {
    return {
      city: [],
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
          value: 2,
        },
        {
          text: "45222",
          value: 3,
        },
        {
          text: "45237",
          value: 4,
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
  created() {
    this.getAllCities();
    if (this.$store.state.token != "") {
      this.user = this.$store.state.userProfile;
    }
  },
  methods: {
    cancelForm() {
      this.$router.push(`/profileDetails`);
    },

    //absolutely take this hash/salt out
    register() {
      const updateUser = {
        passwordHash: this.$store.state.user.passwordHash,
        salt: this.$store.state.user.salt,
        userId: this.$store.state.user.userId,
        role: "user",
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        emailAddress: this.user.emailAddress,
        phoneNumber: this.user.phoneNumber,
        streetAddress: this.user.streetAddress,
        city: parseInt(this.user.city),
        state: parseInt(this.user.state),
        zip: parseInt(this.user.zip),
      };
      //create new user
      if (this.$store.state.token === "") {
        if (this.user.password != this.user.confirmPassword) {
          this.registrationErrors = true;
          this.registrationErrorMsg =
            "Password & Confirm Password do not match.";
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
      }
      //update user
      else {
        userService

          .editUser(updateUser)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
    getAllCities() {
      userService.getAllCities().then((response) => {
        if (response.status === 200) {
          this.city = response.data;
        }
      });
    },
    update() {},
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style></style>