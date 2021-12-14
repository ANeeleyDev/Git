import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import RegisterUser from '../views/RegisterUser.vue'
import PetList from '../views/PetList.vue'
import store from '../store/index'
import AddPet from '../views/AddPet.vue'
import EditPet from '../views/EditPet.vue'
import ProfileDetails from '../views/ProfileDetails.vue'
import PetView from '../views/PetView.vue'
import UserPlaydateView from '../views/UserPlaydateView.vue'
import PlaydateList from '../views/PlaydateList.vue'
import CreatePlaydateForm from '../components/CreatePlaydateForm.vue'
Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },    
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/registerUser",
      name: "register-user",
      component: RegisterUser,
      meta: {
        requiresAuth: false
      }
    },
    {
    path: "/petlist",
    name: "pet-list",
    component: PetList,
    meta: {
      requiresAuth: true
    }

    },
    {
      path: "/addPet",
      name: "add-pet",
      component: AddPet,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/editPet",
      name: "edit-pet",
      component: EditPet,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/profileDetails",
      name: "profile-details",
      component: ProfileDetails,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/petView",
      name: "pet-view",
      component: PetView,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/UserPlaydateView",
      name: "user-playdate-view",
      component: UserPlaydateView,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/playdateList",
      name: "playdate-list",
      component: PlaydateList,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/createPlaydateForm",
      name: "create-playdate-form",
      component: CreatePlaydateForm,
      meta: {
        requiresAuth: false
      }
    },


  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
