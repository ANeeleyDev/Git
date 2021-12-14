<template>
<v-card
    class="mx-auto"
    max-width="400"
  >
    <v-img
      class="white--text align-end"
      height="200px"
      :src="pet.petImage"
    >
      <v-card-title>{{pet.petName}}</v-card-title>
    </v-img>

    <v-card-subtitle class="pb-0">
      {{playdate.meetingTime}}
    </v-card-subtitle>

    <v-card-text class="text--primary">
      <div>{{playdate.playdateAddress}}</div>
    </v-card-text>

    <v-card-text class="text--secondary">
      <div>{{this.playdate.playdateCity}}</div>
    </v-card-text>

    <v-card-actions>
      <v-btn
        color="orange"
        text
      >
        Request Playdate
      </v-btn>

      <v-btn
        color="orange"
        text
      >
        Save
      </v-btn>    
    </v-card-actions>
  </v-card>
</template>

<script>
import playdateService from "@/services/PlaydateService";
export default {
  name: "display-playdates-homepage",
  props: ["pet", "playdate"],

  data() {
    return {
      playdates: {},
      pets: {},
    };
  },
  methods: {
    getAllPlaydatesForDisplay() {
      playdateService.getAllPlaydatesForDisplay().then((response) => {
        this.playdates = response.data;
      });
    },
  },
  created() {
    this.getAllPlaydatesForDisplay();
  },
};
</script> 
