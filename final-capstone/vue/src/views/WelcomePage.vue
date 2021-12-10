<template>
  <v-container>
    <v-row class="text-center">
      <v-col cols="12">
        <v-img
          src="https://d33wubrfki0l68.cloudfront.net/56ffdbe7fabe864ed466a5b9c100455f1dece75e/866b2/static/tdcw-bc-back-white-4e46761f5a4842e1bd81a20ca5b39315.png"
          class="my-3"
          contain
          height="200"
        />
      </v-col>

      <v-col class="mb-4">
        <h1 class="display-2 font-weight-bold mb-3">Welcome to Petonly</h1>

        <!-- <p class="subheading font-weight-regular">
          For help and collaboration with other Vuetify developers,
          <br>please join our online
          <a
            href="https://community.vuetifyjs.com"
            target="_blank"
          >Discord Community</a>
        </p> -->

        <p>Is Spike lonely? He doesn't have to be...</p>
      </v-col>
    </v-row>
    <v-row>
      <v-card class="mx-auto" max-width="500">
        <v-system-bar color="indigo darken-2" dark>
          <v-spacer></v-spacer>

          <v-icon>mdi-window-minimize</v-icon>

          <v-icon>mdi-window-maximize</v-icon>

          <v-icon>mdi-close</v-icon>
        </v-system-bar>

        <v-toolbar color="indigo" dark>
          <v-app-bar-nav-icon></v-app-bar-nav-icon>

          <v-toolbar-title>Discover</v-toolbar-title>

          <v-spacer></v-spacer>

          <v-btn icon>
            <v-icon>mdi-magnify</v-icon>
          </v-btn>
        </v-toolbar>

        <v-container fluid>
          <v-row dense>
            <v-col v-for="card in cards" :key="card.title" :cols="card.flex">
              <v-card>
                <v-img
                  :src="card.src"
                  class="white--text align-end"
                  gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
                  height="200px"
                >
                  <v-card-title v-text="card.title"></v-card-title>
                </v-img>

                <v-card-actions>
                  <v-spacer></v-spacer>

                  <v-btn icon>
                    <v-icon>mdi-heart</v-icon>
                  </v-btn>

                  <v-btn icon>
                    <v-icon>mdi-bookmark</v-icon>
                  </v-btn>

                  <v-btn icon>
                    <v-icon>mdi-share-variant</v-icon>
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-card>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "welcome-page",
  data(){
    return {
      cards: [
        { title: 'Hercules', src: 'https://images.unsplash.com/photo-1611003228941-98852ba62227?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8YmFieSUyMGRvZ3xlbnwwfHwwfHw%3D&w=1000&q=80', flex: 12 },
        { title: 'Aristotle', src: 'https://miniteacuppuppy.com/wp-content/uploads/2020/08/frontbanner1-4.jpg', flex: 6 },
        { title: 'Jennifer', src: 'https://www.akc.org/wp-content/uploads/2018/11/Grooming-tips-from-experts-hero.jpg', flex: 6 },
      ],}}
};
</script>

<style>
</style>