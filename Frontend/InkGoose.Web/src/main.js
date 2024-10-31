import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import Home from './Home.vue'
import Notes from './Notes.vue'
import Login from './components/auth/Login.vue'
import Signup from './components/auth/Signup.vue'
import PrivacyPolicy from './components/PrivacyPolicy.vue'
import Settings from './components/Settings.vue'
import markdownit from 'markdown-it'
import './style.css'
import privacypolicyMd from './md/privacy.md?raw'

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
      name: 'Settings',
      path: '/settings',
      component: Settings,
      meta: {
        requiresAuth: true
      }
    },
    {
      name: 'Privacy policy',
      path: '/privacypolicy',
      component: PrivacyPolicy,
      meta: {
        requiresAuth: false
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

const markdown = markdownit({
  html: true,
  linkify: true,
  typographer: true
});

const app = createApp(App)
app.config.globalProperties.apiHost = import.meta.env.VITE_API_HOST + "/api"
app.config.globalProperties.privacyPolicyHtml = markdown.render(privacypolicyMd, 'utf8')
app.use(router)
app.mount('#app')

