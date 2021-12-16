<template>
  <div>
    <v-row>
      <v-col>
        <v-text-field
          value="playdateCity"
          label="City"
          v-model="filter.playdateCity"
        ></v-text-field>
      </v-col>
      <v-col>
        <v-text-field
          value="zip"
          label="Zipcode"
          v-model="filter.playdateZip"
        ></v-text-field>
      </v-col>
    </v-row>
    
<v-row>
      <v-col v-for="playdate in filteredList" v-bind:key="playdate.playdateId">
          
<<<<<<< HEAD
        <playdate-card v-bind:playdate="playdate" />
=======
        <playdate-card  v-bind:playdate="playdate" />
>>>>>>> 11886ecb909544c7d4ebbbd8baf66de03693a1d4
        </v-col>
      </v-row>
    
  </div>
</template>

<script>
import playdateService from "@/services/PlaydateService";
//import userService from '@/services/UserService';
import PlaydateCard from "@/components/PlaydateCard";
export default {
  components: { PlaydateCard },
  name: "playdate-list",

  data() {
    return {
      playdates: {},
      users: {},
      pet: {},
      currentUserId: this.$store.state.user.userId,
      filter: {
        playdatePostedUser: "",
        playdatePostedPet: "",
        playdateRequestedUserId: "",
        playdateRequestedPetId: "",
        meetingTime: "",
        playdateAddress: "",
        playdateCity: "",
        playdateState: "",
        playdateZip: "",
        playdateStatusId: "",
      },
    };
  },
  methods: {
    getAllPlaydates() {
      playdateService.getAllPlaydatesForDisplay().then((response) => {
        this.playdates = response.data;
      });
    },
  },
  computed: {
    filteredList() {
      let list = this.playdates;

      if (this.filter.playdateCity != "") {
        list = list.filter((playdate) =>
          playdate.playdateCity
            .toLowerCase()
            .includes(this.filter.playdateCity.toLowerCase())
        );
      }
      if (this.filter.playdateZip != "") {
        list = list.filter((playdate) =>
          playdate.playdateZip            
            .includes(this.filter.playdateZip)
        );
      }

      return list;
    },
  },
  created() {
    this.getAllPlaydates();
  },
};
</script>

<style>
</style>