const login={template:`
<div id="container-login">
        <h3>Login</h3>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <label >E-mail:</label>
                    <input type="text" class="form-control"/>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <label >Senha:</label>
                    <input type="password" class="form-control"/>
                </div>
            </div>
        </div>
        <router-link  class="btn btn-link" id="btn-esqueceu" to="/">Esqueceu a senha?</router-link>
        <div class="form-group">
            <div class="row">
                <div class="col-md-6">
                    <button type="button" class="btn btn-primary" id="ovBTN_login">Entrar</button>
                </div>
            </div>
            
        <label>Não possui conta?</label>
        <router-link  class="btn btn-link" id="btn-novo" to="/">Crie sua conta</router-link>
                    
`}