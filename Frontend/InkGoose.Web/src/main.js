import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import Home from './Home.vue'
import Notes from './Notes.vue'
import Login from './components/auth/Login.vue'
import Signup from './components/auth/Signup.vue'
import './style.css'

const router = createRouter({
  routes: [
    {
      name: 'Home',
      path: '/',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      name: 'Notes',
      path: '/notes',
      component: Notes,
      meta: {
        requiresAuth: true
      }
    },
    {
      name: 'Login',
      path: '/login',
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      name: 'Signup',
      path: '/signup',
      component: Signup,
      meta: {
        requiresAuth: false
      }
    }
  ],
  history: createWebHistory()
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!window.localStorage.getItem("accessToken")) {
      next({ name: 'Login' })
    }
    else {
      next()
    }
  }
  else {
    next()
  }
})

const app = createApp(App)
app.config.globalProperties.apiHost = "https://localhost:7264/api"
app.use(router)
app.mount('#app')

