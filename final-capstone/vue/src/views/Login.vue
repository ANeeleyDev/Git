<template>
  <v-card class="mx-auto" max-width="500">
    <h2>Login</h2>
    

    <v-form @submit.prevent="login">
      <v-text-field
        value="username"
        label="username"
        outlined
        v-model="user.username"
        required
        autofocus
      ></v-text-field>
      <v-text-field
        value="password"
        label="password"
        type="password"
        outlined
        v-model="user.password"
        required
      ></v-text-field>
      <v-btn type="submit"> submit </v-btn>
    </v-form>
    <router-link :to="{ name: 'register-user' }"
      ><div>Need an account?</div></router-link
    >
  </v-card>

  <!-- <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
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
      <router-link :to="{ name: 'register' }">Need an account?</router-link>
       <v-btn
        class="mr-4"
        type="submit"
        
      >
        submit
      </v-btn>
    </form>
  </div>  -->
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      currentUser: {},
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>
