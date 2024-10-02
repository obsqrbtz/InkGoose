import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import Home from './Home.vue'
import Login from './components/auth/Login.vue'
import './style.css'

const router = createRouter({
  routes: [
    {
    path: '/',
      component: Home
    },
    {
      path: '/login',
      component: Login
    }
  ],
  history: createWebHistory()
})

const app = createApp(App)
app.config.globalProperties.apiHost = "https://localhost:7264/api"
app.use(router)
app.mount('#app')

